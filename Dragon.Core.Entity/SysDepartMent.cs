using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class SysDepartMent:BaseEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// 部门负责人
        /// </summary>
        ///public string? Leader { get; set; }
        /// <summary>
        /// 上级部门(0表示没有上级部门)
        /// </summary>
        public int Pid { get; set; } = 0;

        public List<SysDepartMent>? Children { get; set; }
    }
}
