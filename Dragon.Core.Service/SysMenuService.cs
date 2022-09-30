using AutoMapper;
using Dragon.Core.Common;
using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class SysMenuService : BaseService<SysMenu>, ISysMenuService
    {
        private readonly IBaseRepository<SysRoleMenuModule> _sysRoleMenuModule;
        readonly IMapper _mapper;
        private readonly IBaseRepository<ApiModule> _apiRepository;
        public SysMenuService(IBaseRepository<SysMenu> baseRepository, IBaseRepository<SysRoleMenuModule> sysRoleMenuModule,IMapper mapper, IBaseRepository<ApiModule> apiRepository) : base(baseRepository)
        {
            _sysRoleMenuModule = sysRoleMenuModule;
            _mapper = mapper;
            _apiRepository = apiRepository;
        }

        public async Task<bool> AddMenuAsync(MenuInput menuInput)
        {
            var sysMenu=_mapper.Map<SysMenu>(menuInput);
            await InsertAsync(sysMenu);
            return true;
        }

        public async Task<List<MenuListItem>> GetMenuList(MenuParams menuParams)
        {
            Expression<Func<SysMenu, bool>> e = b => b.IsDrop == false;
            string key = menuParams.title;
            int menuType = menuParams.menuType;
            if (!string.IsNullOrEmpty(key) && menuType > -1)
            {
                e = b => b.IsDrop == false && b.Title.Contains(key) && b.MenuType == (MenuTypeEnum)menuType;
            }

            if (!string.IsNullOrWhiteSpace(key))
            {
                e = b => b.IsDrop == false && b.Title.Contains(key);
            }

            if (menuType > -1)
            {
                e = b => b.IsDrop == false && b.MenuType == (MenuTypeEnum)menuType;
            }
            var MenuLists = await GetListAsync(e);
            List<MenuListItem> menuModels = _mapper.Map<List<MenuListItem>>(MenuLists);
            var apiModuleList = await _apiRepository.GetListAsync();
            foreach (MenuListItem item in menuModels)
            {
                int apiId = item.mid;
                item.apiName = apiModuleList.FirstOrDefault(d => d.Id == apiId)?.ApiName;
            }
            menuModels= menuModels.ToTree((r) => { return r.pid == 0; }, (r, c) => { return r.id == c.pid; }, (r, dataList) =>
            {
                r.children  ??= new List<MenuListItem>();
                r.children.AddRange(dataList);

            });

            return menuModels;
        }

        /// <summary>
        /// 得到权限码
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public async Task<List<string?>> GetPermCodes(List<int> roleIds)
        {
            var menuModules = await _sysRoleMenuModule.GetListAsync(d=>roleIds.Contains(d.RoleId));
            List<int> menuIds = menuModules.Select(d => d.MenuId).ToList();
            var menusList=await GetListAsync(d=>menuIds.Contains(d.Id) && d.MenuType==MenuTypeEnum.Btn);
            List<string?> permCodes = menusList.Select(d=>d.Permission).ToList();
            return permCodes;
        }

        public async Task<List<MenuTreeViewModel>> GetTreeMenuList()
        {
            var menuList=await GetListAsync(d=>d.IsDrop==false && d.MenuType!=MenuTypeEnum.Btn);

            menuList = menuList.ToTree((r) => { return r.ParId == 0; }, (r, c) => { return r.Id == c.ParId; }, (r, dataList) =>
            {
                r.Children ??= new List<SysMenu>();
                r.Children.AddRange(dataList);

            });
            var menuTreeList = _mapper.Map<List<MenuTreeViewModel>>(menuList);
            return menuTreeList;
        }

        public async Task<bool> UpdateMenuAsync(MenuInput menuInput)
        {
            var sysMenu = _mapper.Map<SysMenu>(menuInput);
            sysMenu.UpdateTime=DateTime.Now;
            await UpdateAsync(sysMenu);
            return true;
        }
    }
}
