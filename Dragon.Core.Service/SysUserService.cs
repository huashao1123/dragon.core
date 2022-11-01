using AutoMapper;
using Dragon.Core.Common;
using Dragon.Core.Entity;
using Dragon.Core.Entity.Enum;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        readonly IUser _user;
        readonly IMapper _mapper;
        readonly IDepartMentRepository _sysDepartMentRepository;
        readonly IBaseRepository<SysUserDept> _sysUserDeptRepository;
        readonly IBaseRepository<SysRoleDept> _roleDeptRepository;
        readonly IBaseRepository<SysRole> _roleRepository;
        readonly IUserRoleRepository _userRoleRepository;
        public SysUserService(IBaseRepository<SysUser> baseRepository,IUser user, IMapper mapper, IDepartMentRepository sysDepartMentRepository, IBaseRepository<SysUserDept> sysUserDeptRepository, IBaseRepository<SysRoleDept> roleDeptRepository, IBaseRepository<SysRole> roleRepository, IUserRoleRepository userRoleRepository) : base(baseRepository)
        {
            _user = user;
            _mapper = mapper;
            _sysDepartMentRepository = sysDepartMentRepository;
            _sysUserDeptRepository = sysUserDeptRepository;
            _roleDeptRepository = roleDeptRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<bool> AddUserAsynce(UserInput userInput)
        {
           
            var user =await FindAsync(d => d.Account == userInput.account && d.IsDrop==false);
            if (user != null )
            {
                throw new UserFriendlyException("账户已存在");
            }
            SysUser sysUser = _mapper.Map<SysUser>(userInput);
            sysUser.CreatedName = _user.Name;
            sysUser.CreatedTime = DateTime.Now;
            sysUser.Password = MD5Util.GetMD5_32(CommonConst.SysPassword);            
            await InsertAsync(sysUser);
            return true;
        }

        public async Task<PageModel<UserViewModel>> GetPageUserListAsynce(UserPageInput userPageInput)
        {
            int deptId = userPageInput.DepartmentId;
            var deptIdList = deptId > 0 ? await _sysDepartMentRepository.GetChildDeptIdListWithSelfById(deptId) : new List<int>();
            string? key = userPageInput.name;
            string? phone = userPageInput.Mobile;
            var query=_baseRepository.Table.Where(u=>u.IsDrop==false).WhereIf(u=>u.Account.Contains(key) || u.Name.Contains(key),!string.IsNullOrWhiteSpace(key)).WhereIf(u=>u.Mobile.Contains(phone),!string.IsNullOrWhiteSpace(phone)).WhereIf(u=>deptIdList.Contains(u.DepartmentId),deptId>0).WhereIf(u=>u.UserType!=UserTypeEnum.SuperAdmin,!_user.IsSuperAdmin);
            int count = await query.CountAsync().ConfigureAwait(false);
            var pageQuery=await query.ToPageListAsync(userPageInput.Page,userPageInput.PageSize);
            var models = _mapper.Map<List<UserViewModel>>(pageQuery);
            return new PageModel<UserViewModel>(models, userPageInput.Page, userPageInput.PageSize, count);
        }

        public async Task<bool> UpdateUserAsync(UserInput userInput)
        {
            var user = await FindAsync(d => d.Account == userInput.account && d.IsDrop == false && d.Id!=userInput.id);
            if (user != null)
            {
                throw new UserFriendlyException("账户已存在");
            }
            SysUser sysUser = _mapper.Map<SysUser>(userInput);
            sysUser.UpdateName = _user.Name;
            sysUser.UpdateTime = DateTime.Now;
            int count = await UpdateNotQueryAsync(sysUser, isIgnoreCol: true, properties: new Expression<Func<SysUser, object>>[] { d => d.CreatedName, d => d.CreatedTime,d=>d.UserType,d=>d.Password });
            return count > 0;
        }

        public async Task<bool> GrantUserDeptAsync(UserDeptInput userDeptInput)
        {
            await _sysUserDeptRepository.DeleteAsync(u => u.UserId == userDeptInput.Id);
            var data = userDeptInput.DeptIdList.Select(u => new SysUserDept
            {
                UserId=userDeptInput.Id,
                DeptId=u
            });
            await _sysUserDeptRepository.InsertManyAsync(data,true);
            return true;
        }

        public async Task<List<int>>OwnDeptIdListAsync(int userId)
        {
            var userDeptList = await _sysUserDeptRepository.GetListAsync(u => u.UserId == userId);
            var deptIdList= userDeptList.Select(u=>u.DeptId).ToList();
            return deptIdList;
        }

        public async Task CheckDataScope(int deptId)
        {
            if (!_user.IsSuperAdmin)
            {
                var deptIdList =await GetDeptIdList();
                if (!deptIdList.Contains(deptId))
                {
                    throw new UserFriendlyException($"{_user.Name}没有权限分配这个用户的部门");
                }
            }
        }

        public async Task<List<int>>GetDeptIdList()
        {
            int userId = Convert.ToInt32(_user.ID);
            var userDeptIdList = await OwnDeptIdListAsync(Convert.ToInt32(_user.ID));
            var roleIdList= await _userRoleRepository.GetRoleIdListAsync(userId);
            int deptId = (await GetEntityAsync(userId))?.DepartmentId ?? -1;
            var deptIdList = await GetDeptByRoleId(deptId, roleIdList);
            return userDeptIdList.Union(deptIdList).ToList();
        }

        private async Task<List<int>>GetDeptByRoleId(int deptId, List<int> roleIdList)
        {
            var roleList= await _roleRepository.GetListAsync(r=>roleIdList.Contains(r.Id));
            // 按最大范围策略设定(如果同时拥有ALL和SELF的权限，则结果ALL)
            int strongerDataScopeType = (int)DataScopeEnum.Self;
            // 数据范围拥有的角色集合
            var customDataScopeRoleIdList = new List<int>();
            if (roleList != null && roleList.Count > 0)
            {
                roleList.ForEach(u =>
                {
                    if (u.DataScope == DataScopeEnum.Define)
                        customDataScopeRoleIdList.Add(u.Id);
                    else if ((int)u.DataScope <= strongerDataScopeType)
                        strongerDataScopeType = (int)u.DataScope;
                });
            }
            var deptIdList = await _sysDepartMentRepository.GetDeptIdListByDataScope(strongerDataScopeType, deptId);
            var deptIdList2= await _roleDeptRepository.Table.Where((d => customDataScopeRoleIdList.Contains(d.RoleId))).Select(d => d.DeptId).ToListAsync();
            return deptIdList.Union(deptIdList2).ToList();
        }

        public async Task DeleteDeptByUserId(int userId)
        {
            await _sysUserDeptRepository.DeleteAsync(d => d.UserId == userId);
        }
    }
}
