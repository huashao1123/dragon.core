using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    /// <summary>
    /// 菜单树
    /// </summary>
    public class MenuTreeViewModel
    {
        /// <summary>
        ///  菜单名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 路由组件
        /// </summary>
        public string component { get; set; }
        /// <summary>
        /// 重定向
        /// </summary>
        public string? redirect { get; set; }

        public MenuMetaViewModel meta { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuTreeViewModel>? children { get; set; }
    }
    /// <summary>
    /// 菜单元素
    /// </summary>
    public class MenuMetaViewModel
    {
        /// <summary>
        /// 路由title  一般必填,显示菜单名称的
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 动态路由可打开Tab页数
        /// </summary>
        public string? dynamicLevel { get; set; }
        /// <summary>
        /// 动态路由的实际Path, 即去除路由的动态部分;
        /// </summary>
        public string? realPath { get; set; }
        /// <summary>
        /// 是否忽略KeepAlive缓存
        /// </summary>
        public bool? ignoreKeepAlive { get; set; }
        /// <summary>
        /// 是否固定标签
        /// </summary>
        public bool? affix { get; set; }
        /// <summary>
        ///  图标，也是菜单图标
        /// </summary>
        public string? icon { get; set; }
        /// <summary>
        /// 内嵌iframe的地址
        /// </summary>
        public string? frameSrc { get; set; }
        /// <summary>
        /// 指定该路由切换的动画名
        /// </summary>
        public string? transitionName { get; set; }
        /// <summary>
        /// 隐藏该路由在面包屑上面的显示
        /// </summary>
        public string? hideBreadcrumb { get; set; }
        /// <summary>
        /// 如果该路由会携带参数，且需要在tab页上面显示。则需要设置为true
        /// </summary>
        public bool? carryParam { get; set; }
        /// <summary>
        /// 隐藏所有子菜单
        /// </summary>
        public bool? hideChildrenInMenu { get; set; }
        /// <summary>
        /// 当前激活的菜单。用于配置详情页时左侧激活的菜单路径
        /// </summary>
        public string? currentActiveMenu { get; set; }
        /// <summary>
        /// 当前路由不再标签页显示
        /// </summary>
        public bool? hideTab { get; set; }
        /// <summary>
        /// 当前路由不再菜单显示
        /// </summary>
        public bool? hideMenu { get; set; }
        /// <summary>
        /// 忽略路由。用于在ROUTE_MAPPING以及BACK权限模式下，生成对应的菜单而忽略路由。
        /// </summary>
        public bool? ignoreRoute { get; set; }
        /// <summary>
        /// 是否在子级菜单的完整path中忽略本级path。
        /// </summary>
        public bool? hidePathForChildren { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int orderNo { get; set; }
    }
}
