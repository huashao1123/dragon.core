using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 角色拥有的自定义数据权限表
    /// </summary>
    public class SysRoleDept:BaseEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public int DeptId { get; set; }
    }
}
