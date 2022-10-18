using AutoMapper;
using Dragon.Core.Common;
using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
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
        public SysUserService(IBaseRepository<SysUser> baseRepository,IUser user, IMapper mapper, IDepartMentRepository sysDepartMentRepository, IBaseRepository<SysUserDept> sysUserDeptRepository) : base(baseRepository)
        {
            _user = user;
            _mapper = mapper;
            _sysDepartMentRepository = sysDepartMentRepository;
            _sysUserDeptRepository = sysUserDeptRepository;
        }

        public async Task<bool> AddUserAsynce(UserInput userInput)
        {
            SysUser sysUser=_mapper.Map<SysUser>(userInput);
            sysUser.CreatedName = _user.Name;
            sysUser.CreatedTime = DateTime.Now;
            await InsertAsync(sysUser);
            return true;
        }

        public async Task<PageModel<UserViewModel>> GetPageUserListAsynce(UserPageInput userPageInput)
        {
            int deptId = userPageInput.DepartmentId;
            var deptIdList = deptId > 0 ? await _sysDepartMentRepository.GetChildDeptIdListWithSelfById(deptId) : null;
            string key = userPageInput.name;
            string phone = userPageInput.Mobile;
            var query=_baseRepository.Table.Where(u=>u.IsDrop==false).WhereIf(u=>u.Account.Contains(key) || u.Name.Contains(key),!string.IsNullOrWhiteSpace(phone)).WhereIf(u=>u.Mobile.Contains(phone),!string.IsNullOrWhiteSpace(phone)).WhereIf(u=>deptIdList.Contains(u.DepartmentId),deptId>0);
            int count = await query.CountAsync().ConfigureAwait(false);
            var pageQuery=await query.ToListAsync(userPageInput.Page,userPageInput.PageSize);
            var models = _mapper.Map<List<UserViewModel>>(pageQuery);
            return new PageModel<UserViewModel>(models, userPageInput.Page, userPageInput.PageSize, count);
        }

        public async Task<bool> UpdateUserAsync(UserInput userInput)
        {
            SysUser sysUser = _mapper.Map<SysUser>(userInput);
            sysUser.UpdateName = _user.Name;
            sysUser.UpdateTime = DateTime.Now;
            int count = await UpdateNotQueryAsync(sysUser, isIgnoreCol: true, properties: new Expression<Func<SysUser, object>>[] { d => d.CreatedName, d => d.CreatedTime,d=>d.UserType });
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
            await _sysUserDeptRepository.InsertManyAsync(data);
            return true;
        }

        public async Task<List<int>>OwnDeptIdListAsync(int userId)
        {
            var userDeptList = await _sysUserDeptRepository.GetListAsync(u => u.UserId == userId);
            var deptIdList= userDeptList.Select(u=>u.DeptId).ToList();
            return deptIdList;
        }
    }
}
