using Dragon.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Permissions.Name)]
    [ApiController]
    public class SysMenuController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IUserRoleService _roleService;
        private readonly ISysMenuService _sysMenuService;
        public SysMenuController(IUser user, IUserRoleService roleService, ISysMenuService sysMenuService)
        {
            _user = user;
            _roleService = roleService;
            _sysMenuService = sysMenuService;
        }
        /// <summary>
        /// 获取按钮权限(登录)
        /// </summary>
        /// <returns></returns>
        [HttpGet("/getPermCode")]
        public async Task<MessageModel<List<string?>>>GetPermCode()
        {
            int userId=Convert.ToInt32(_user.ID);
            List<int> roleIds =await _roleService.GetRoleId(userId);
            var model = new MessageModel<List<string?>>();
            model.result =await _sysMenuService.GetPermCodes(roleIds);
            return model;
        }
        [HttpGet("/getMenuList")]
        public async Task<MessageModel<List<MenuTreeViewModel>>> GetLoginMenuTree()
        {
            int userId = Convert.ToInt32(_user.ID);
            List<int> roleIds = await _roleService.GetRoleId(userId);//到时根据角色来显示菜单
            var model = new MessageModel<List<MenuTreeViewModel>>();
            model.result = await _sysMenuService.GetTreeMenuList();
            return model;
        }

        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="menuParams"></param>
        /// <returns></returns>
        [HttpGet("/sysMenu/list")]
        public async Task<MessageModel<List<MenuListItem>>> MenuListAsync([FromQuery] MenuParams menuParams)
        {
            var menuList = await _sysMenuService.GetMenuList(menuParams);
            var data = new MessageModel<List<MenuListItem>>();
            data.result = menuList;
            return data;
        }


        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysMenu/add")]
        public async Task<MessageModel<bool>> AddMenuAsync(MenuInput input)
        {
            var data = new MessageModel<bool>();
            input.createdName = _user.Name;
            var res = await _sysMenuService.AddMenuAsync(input);
            data.result = res;
            return data;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("/sysMenu/update")]
        public async Task<MessageModel<bool>> UpdateMenuAsync(MenuInput input)
        {
            var data = new MessageModel<bool>();
            var res = await _sysMenuService.UpdateMenuAsync(input);
            data.result = res;
            return data;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/sysMenu/delete/{id}")]
        public async Task<MessageModel<bool>> DeleteMenuAsync(int id)
        {
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _sysMenuService.FindAsync(d => d.Id == id);
            if (entity != null)
            {
                entity.IsDrop = true;
                await _sysMenuService.UpdateAsync(entity);
                data.result = true;
            }
            return data;
        }
    }
}
