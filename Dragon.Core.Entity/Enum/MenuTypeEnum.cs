using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 系统菜单类型枚举
    /// </summary>
    public enum MenuTypeEnum
    {
        /// <summary>
        /// 目录
        /// </summary>
        [Description("目录")]
        Dir = 1,

        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 2,

        /// <summary>
        /// 按钮
        /// </summary>
        [Description("按钮")]
        Btn = 3
    }
}
