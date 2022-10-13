using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// 菜单权限表
    /// </summary>
    public class SysMenu:BaseEntity
    {
        /// <summary>
        ///  菜单名
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 菜单类型（1目录 2菜单 3按钮）
        /// </summary>
        public MenuTypeEnum MenuType { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string path { get; set; } = "";
        /// <summary>
        /// 路由组件
        /// </summary>
        public string? Component { get; set; }
        /// <summary>
        /// 权限标识（权限码）
        /// </summary>
        public string? Permission { get; set; }
        /// <summary>
        /// 重定向
        /// </summary>
        public string? Redirect { get; set; }
        /// <summary>
        /// 内嵌地址
        /// </summary>
        public string? FrameSrc { get; set; }

        /// <summary>
        /// 路由title  一般必填
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///  图标，也是菜单图标
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// 是否忽略KeepAlive缓存
        /// </summary>
        public bool? IgnoreKeepAlive { get; set; }
        /// <summary>
        /// 当前路由不再菜单显示
        /// </summary>
        public bool? HideMenu { get; set; }

        /// <summary>
        /// 当前激活的菜单。用于配置详情页时左侧激活的菜单路径
        /// </summary>
        public string? CurrentActiveMenu { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// </summary>
        public int ParId { get; set; } = 0;
        /// <summary>
        /// api地址接口
        /// </summary>
        public int Mid { get; set; } = 0;

        public List<SysMenu>? Children { get; set; }
    }
}
