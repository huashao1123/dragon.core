using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Repository
{
    public class UserRoleRepository : BaseRepository<SysUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IUintOfWork uintOfWork) : base(uintOfWork)
        {
        }

        public async Task<List<int>> GetRoleIdListAsync(int userId)
        {
            var userRoles = await GetListAsync(d => d.UserId == userId);
            List<int> roleIds = userRoles.Select(d => d.RoleId).ToList();
            return roleIds;
        }
    }
}
