using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IService
{
    public interface ISysRoleMenuService : IBaseService<SysRoleMenuModule>
    {
        Task<List<MenuListItem>> GetMenuList(int roleId);

        Task<bool> GrantMenu(RoleMenuInPut roleMenuInPut);
    }
}
