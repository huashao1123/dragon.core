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
        private readonly IUser _user;
        public SysMenuService(IBaseRepository<SysMenu> baseRepository, IBaseRepository<SysRoleMenuModule> sysRoleMenuModule,IMapper mapper, IBaseRepository<ApiModule> apiRepository, IUser user) : base(baseRepository)
        {
            _sysRoleMenuModule = sysRoleMenuModule;
            _mapper = mapper;
            _apiRepository = apiRepository;
            _user = user;
        }

        public async Task<bool> AddMenuAsync(MenuInput menuInput)
        {
            CheckMenuParam(menuInput);
            var menuList= menuInput.menuType==(int)MenuTypeEnum.Btn ?await _baseRepository.GetListAsync(d=>d.IsDrop==false&& d.Permission==menuInput.permission): await _baseRepository.GetListAsync(d => d.IsDrop == false && d.Title==menuInput.title);
            if (menuList.Count>0)
            {
                throw new UserFriendlyException("数据重复");
            }

            var sysMenu=_mapper.Map<SysMenu>(menuInput);
            sysMenu.Name ??= "";
            sysMenu.path ??= "";
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

            List<int> menuIds =await GetRoleMenuIdList(roleIds);
            var menusList=await GetListAsync(d=>d.IsDrop==false && d.MenuType==MenuTypeEnum.Btn);
            List<string?> permCodes = menusList.WhereIf(d=>menuIds.Contains(d.Id),!_user.IsSuperAdmin).Select(d=>d.Permission).ToList();
            return permCodes;
        }


        private async Task<List<int>>GetRoleMenuIdList(List<int> roleIds)
        {
            var menuModules = await _sysRoleMenuModule.GetListAsync(d => roleIds.Contains(d.RoleId));
            List<int> menuIds = _user.IsSuperAdmin ? new List<int>() : menuModules.Select(d => d.MenuId).ToList();
            return menuIds;
        }


        public async Task<List<MenuTreeViewModel>> GetTreeMenuList(List<int> roleIds)
        {
            var menuList=await GetListAsync(d=>d.IsDrop==false && d.MenuType!=MenuTypeEnum.Btn);
            List<int> menuIds = await GetRoleMenuIdList(roleIds);
            menuList = menuList.WhereIf(d => menuIds.Contains(d.Id),!_user.IsSuperAdmin).ToList().ToTree((r) => { return r.ParId == 0; }, (r, c) => { return r.Id == c.ParId; }, (r, dataList) =>
            {
                r.Children ??= new List<SysMenu>();
                r.Children.AddRange(dataList);

            });
            var menuTreeList = _mapper.Map<List<MenuTreeViewModel>>(menuList);
            return menuTreeList;
        }

        public async Task<bool> UpdateMenuAsync(MenuInput menuInput)
        {
            CheckMenuParam(menuInput);
            var menuList = menuInput.menuType == (int)MenuTypeEnum.Btn ? await _baseRepository.GetListAsync(d => d.IsDrop == false && d.Permission == menuInput.permission && d.Id!=menuInput.id) : await _baseRepository.GetListAsync(d => d.IsDrop == false && d.Title == menuInput.title && d.Id != menuInput.id);
            if (menuList.Count > 0)
            {
                throw new UserFriendlyException("数据重复");
            }
            var sysMenu = _mapper.Map<SysMenu>(menuInput);
            sysMenu.UpdateTime=DateTime.Now;
            sysMenu.Name ??= "";
            sysMenu.path ??= "";
            await UpdateAsync(sysMenu);
            return true;
        }

        private void CheckMenuParam(MenuInput menuInput)
        {
            if (menuInput.menuType==(int)MenuTypeEnum.Btn)
            {
                string? permission = menuInput.permission;
                if (string.IsNullOrWhiteSpace(permission))
                {
                    throw new UserFriendlyException("权限标识为空");
                }
                else if (!permission.Contains(":"))
                {
                    throw new UserFriendlyException("权限标识格式错误");
                }
            }
        }
    }
}
