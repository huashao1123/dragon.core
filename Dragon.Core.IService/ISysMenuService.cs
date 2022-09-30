namespace Dragon.Core.IService
{
    public interface ISysMenuService:IBaseService<SysMenu>
    {
        /// <summary>
        /// 根据角色id,获取按钮权限
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public Task<List<string?>> GetPermCodes(List<int> roleIds);
        /// <summary>
        /// 获取登录菜单树
        /// </summary>
        /// <returns></returns>
        public Task<List<MenuTreeViewModel>> GetTreeMenuList();
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="menuParams"></param>
        /// <returns></returns>
        public Task<List<MenuListItem>> GetMenuList(MenuParams menuParams);
        /// <summary>
        /// 增加菜单
        /// </summary>
        /// <param name="menuInput"></param>
        /// <returns></returns>
        public Task<bool> AddMenuAsync(MenuInput menuInput);
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menuInput"></param>
        /// <returns></returns>
        public Task<bool> UpdateMenuAsync(MenuInput menuInput);
    }
}
