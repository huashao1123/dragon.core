using AutoMapper;
using Dragon.Core.Common;
using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class SysRoleMenuService : BaseService<SysRoleMenuModule>, ISysRoleMenuService
    {
        readonly IBaseRepository<SysMenu> _menuRepository;
        readonly IMapper _mapper;
        public SysRoleMenuService(IBaseRepository<SysRoleMenuModule> baseRepository, IBaseRepository<SysMenu> menuRepository,IMapper mapper) : base(baseRepository)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuListItem>> GetMenuList(int roleId)
        {
            List<int> MenuIdList= (await GetListAsync(d=>d.RoleId==roleId)).Select(d=>d.MenuId).ToList();
            var menuList = await _menuRepository.GetListAsync(d => d.IsDrop == false && MenuIdList.Contains(d.Id));
            var menuModels=_mapper.Map<List<MenuListItem>>(menuList);
            menuModels = menuModels.ToTree((r) => { return r.pid == 0; }, (r, c) => { return r.id == c.pid; }, (r, dataList) =>
            {
                r.children ??= new List<MenuListItem>();
                r.children.AddRange(dataList);

            });
            return menuModels;
        }

        public async Task<bool> GrantMenu(RoleMenuInPut roleMenuInPut)
        {
            await DeleteAsync(d => d.RoleId == roleMenuInPut.Id);
            var menuList=await _menuRepository.GetListAsync();
            var menus = roleMenuInPut.MenuIdList.Select(u => new SysRoleMenuModule
            {
                RoleId = roleMenuInPut.Id,
                Mid=menuList.Where(d=>d.Id==u).Select(d=>d.Mid).FirstOrDefault(),
                MenuId = u
            }).ToList();
            await InsertManyAsync(menus);
            return true;
        }
    }
}
