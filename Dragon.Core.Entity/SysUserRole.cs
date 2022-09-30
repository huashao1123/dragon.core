using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 用户角色关系对应表
    /// </summary>
    public class SysUserRole:BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
    }
}
