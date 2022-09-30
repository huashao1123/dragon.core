using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 角色菜单接口权限关系对应表
    /// </summary>
    public class SysRoleMenuModule:BaseEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 菜单权限Id
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// api接口Id,后台接口鉴权用
        /// </summary>
        public int Mid { get; set; }
    }
}
