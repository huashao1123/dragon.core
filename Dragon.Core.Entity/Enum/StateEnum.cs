using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Dragon.Core.Entity.Enum
{
    public enum StateEnum
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Normal = 0,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Frozen = 1,
    }
}
