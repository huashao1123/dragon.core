using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 用户部门关系对应表
    /// </summary>
    public class SysUserDepartMent:BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartMentId { get; set; }    
    }
}
