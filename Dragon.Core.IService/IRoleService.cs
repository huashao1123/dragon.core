using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IService
{
    public interface IRoleService:IBaseService<SysRole>
    {
        Task<PageModel<SysRole>> GetPageRoleListAsync(RolePageInput rolePageInput);

        Task<bool> AddRoleAsync(RoleInput role);

        Task<bool> UpdateRoleAsync(UpdateRoleInput role);
    }
}
