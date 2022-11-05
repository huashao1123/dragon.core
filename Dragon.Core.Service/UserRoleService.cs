using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class UserRoleService :IUserRoleService
    {
        private readonly IBaseRepository<SysRole> _sysRoleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleService(IBaseRepository<SysRole> sysRoleRepository,IUserRoleRepository userRoleRepository) 
        {
            _sysRoleRepository = sysRoleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task DeleteAsync(Expression<Func<SysUserRole, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            await _userRoleRepository.DeleteAsync(predicate, autoSave, cancellationToken);
        }

        public async Task<List<int>> GetRoleId(int userId)
        {
            List<int> roleIds =await _userRoleRepository.GetRoleIdListAsync(userId);
            return roleIds;
        }

        public async Task<List<RoleInfo>> GetRoleInfoList(int userId)
        {
            List<RoleInfo> roleInfoList = new List<RoleInfo>();
            var userRoles = await _userRoleRepository.GetListAsync(d => d.UserId == userId);
            var rolelist = await _sysRoleRepository.GetListAsync();
            if (userRoles.Count > 0)
            {
                var arr = userRoles.Select(d => d.RoleId).ToArray();
                var roles = rolelist.Where(d => arr.Contains(d.Id));
               roleInfoList=roles.Select(r=>new RoleInfo { Id=r.Id, Name=r.Name,Code=r.Code}).ToList();
            }
            return roleInfoList;
        }

        /// <summary>
        /// 得到角色名，以,分割
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> GetUserRoleNames(int userId)
        {
            string roleNames = "";
            var userRoles = await _userRoleRepository.GetListAsync(d => d.UserId == userId);
            var rolelist = await _sysRoleRepository.GetListAsync();
            if (userRoles.Count > 0)
            {
                var arr = userRoles.Select(d => d.RoleId).ToArray();
                var roles = rolelist.Where(d => arr.Contains(d.Id));
                roleNames = String.Join(",", roles.Select(r => r.Name).ToArray());
            }
            return roleNames;
        }

        public async Task<bool> GrantRole(UserRoleInput userRoleInput)
        {
            await _userRoleRepository.DeleteAsync(d=>d.UserId==userRoleInput.Id);
            var data = userRoleInput.RoleIdList.Select(u => new SysUserRole
            {
                UserId = userRoleInput.Id,
                RoleId = u
            }).ToList();
            await _userRoleRepository.InsertManyAsync(data,true);
            return true;
        }
    }
}
