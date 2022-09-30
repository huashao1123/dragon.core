using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class UserRoleService : BaseService<SysUserRole>, IUserRoleService
    {
        private readonly IBaseRepository<SysRole> _sysRoleRepository;
        public UserRoleService(IBaseRepository<SysRole> sysRoleRepository, IBaseRepository<SysUserRole> baseRepository) : base(baseRepository)
        {
            _sysRoleRepository = sysRoleRepository;
        }

        public async Task<List<int>> GetRoleId(int userId)
        {
            var userRoles = await GetListAsync(d => d.UserId == userId);
            List<int> roleIds = userRoles.Select(d => d.RoleId).ToList();
            return roleIds;
        }

        public async Task<List<RoleInfo>> GetRoleInfoList(int userId)
        {
            List<RoleInfo> roleInfoList = new List<RoleInfo>();
            var userRoles = await GetListAsync(d => d.UserId == userId);
            var rolelist = await _sysRoleRepository.GetListAsync();
            if (userRoles.Count > 0)
            {
                var arr = userRoles.Select(d => d.RoleId).ToArray();
                var roles = rolelist.Where(d => arr.Contains(d.Id));
               roleInfoList=roles.Select(r=>new RoleInfo { RoleName=r.Name,Value=r.Code}).ToList();
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
            var userRoles = await GetListAsync(d => d.UserId == userId);
            var rolelist = await _sysRoleRepository.GetListAsync();
            if (userRoles.Count > 0)
            {
                var arr = userRoles.Select(d => d.RoleId).ToArray();
                var roles = rolelist.Where(d => arr.Contains(d.Id));
                roleNames = String.Join(",", roles.Select(r => r.Name).ToArray());
            }
            return roleNames;
        }


    }
}
