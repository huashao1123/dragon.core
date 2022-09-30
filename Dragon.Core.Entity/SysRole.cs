using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class SysRole:BaseEntity
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 数据范围（1全部数据 2本部门及以下数据 3本部门数据 4仅本人数据 5自定义数据）
        /// </summary>
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.Self;
    }
}
