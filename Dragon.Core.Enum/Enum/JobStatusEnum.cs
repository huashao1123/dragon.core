using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity.Enum
{
    /// <summary>
    /// 岗位状态枚举
    /// </summary>
    public enum JobStatusEnum
    {
        /// <summary>
        /// 在职
        /// </summary>
        [Description("在职")]
        On = 1,

        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        Off = 2,

        /// <summary>
        /// 请假
        /// </summary>
        [Description("请假")]
        Leave = 3,
    }
}
