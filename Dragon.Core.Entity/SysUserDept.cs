using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 用户拥有的自定义数据权限
    /// </summary>
    public class SysUserDept:BaseEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public int DeptId { get; set; }
    }
}
