using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 角色数据范围枚举
    /// </summary>
    public enum DataScopeEnum
    {
        /// <summary>
        /// 全部数据
        /// </summary>
        [Description("全部数据")]
        All = 1,

        /// <summary>
        /// 本部门及以下数据
        /// </summary>
        [Description("本部门及以下数据")]
        Dept_with_child = 2,

        /// <summary>
        /// 本部门数据
        /// </summary>
        [Description("本部门数据")]
        Dept = 3,

        /// <summary>
        /// 仅本人数据
        /// </summary>
        [Description("仅本人数据")]
        Self = 4,

        /// <summary>
        /// 自定义数据
        /// </summary>
        [Description("自定义数据")]
        Define = 5
    }
}
