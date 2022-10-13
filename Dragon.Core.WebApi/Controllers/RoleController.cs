using Dragon.Core.Entity;
using Dragon.Core.Entity.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class RoleController : ControllerBase
    {
        readonly IRoleService _roleService;
        readonly ISysRoleMenuService _sysRoleMenuService;
        readonly ISysRoleDeptService _sysRoleDeptService;
        public RoleController(IRoleService roleService, ISysRoleMenuService sysRoleMenuService, ISysRoleDeptService sysRoleDeptService)
        {
            _roleService = roleService;
            _sysRoleMenuService = sysRoleMenuService;
            _sysRoleDeptService = sysRoleDeptService;
        }
        [HttpGet("/sysrole/pagerolelist")]
        public async Task<MessageModel<PageModel<SysRole>>> GetPageRoleListAsync([FromQuery] RolePageInput rolePageInput)
        {
            var messageModel = new MessageModel<PageModel<SysRole>>();
            messageModel.result= await _roleService.GetPageRoleListAsync(rolePageInput);
            return messageModel;
        }

        [HttpPost("/sysrole/add")]
        public async Task<MessageModel<bool>>AddRoleAsync(RoleInput role)
        {
            var messageModel=new MessageModel<bool>();
            messageModel.result= await _roleService.AddRoleAsync(role);
            return messageModel;
        }

        [HttpPut("/sysrole/update")]
        public async Task<MessageModel<bool>>UpdateRoleAsync(UpdateRoleInput updateRoleInput)
        {
            var messageModel = new MessageModel<bool>();
            messageModel.result = await _roleService.UpdateRoleAsync(updateRoleInput);
            return messageModel;
        }
        [HttpDelete("/sysrole/delete")]
        public async Task<MessageModel<bool>> DeleteRoleAsync([FromRoute]int id)
        {
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _roleService.FindAsync(d => d.Id == id);
            if (entity != null)
            {
                entity.IsDrop = true;
                await _roleService.UpdateAsync(entity);
                data.result = true;
            }
            return data;
        }

        /// <summary>
        /// 设置角色状态
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("/sysRole/setStatus")]
        public async Task<MessageModel<bool>>SetRoleStatus(RoleInput role)
        {
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _roleService.FindAsync(d => d.Id == role.Id);
            if (entity != null)
            {
                entity.Status = (StateEnum)role.Status;
                await _roleService.UpdateAsync(entity);
                data.result = true;
            }
            return data;
        }
        /// <summary>
        /// 根据角色Id获取菜单树(前端区分父子节点)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/sysRole/ownmenu")]
        public async Task<MessageModel<List<MenuListItem>>> GetRoleOwnMenuListAsync(int id)
        {
            var menuList=await _sysRoleMenuService.GetMenuList(id);
            MessageModel<List<MenuListItem>> data = new MessageModel<List<MenuListItem>>();
            data.result = menuList;
            return data;
        }
        /// <summary>
        /// 根据角色授权菜单
        /// </summary>
        /// <param name="inPut"></param>
        /// <returns></returns>
        [HttpPost("/sysrole/grantmenu")]
        public async Task<MessageModel<bool>>GrantRoleMenuAsync(RoleMenuInPut inPut)
        {
            var res=await _sysRoleMenuService.GrantMenu(inPut);
            MessageModel<bool> data = new MessageModel<bool>();
            data.result=res;
            return data;
        }
        /// <summary>
        /// 根据角色Id获取机构Id集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/sysRole/ownDept")]
        public async Task<MessageModel<List<int>>> GetRoleDeptListAsync(int id)
        {
            var deptIdList=await _sysRoleDeptService.GetDeptIdList(id);
            var data = new MessageModel<List<int>>();
            data.result = deptIdList;
            return data;
        }
        /// <summary>
        /// 授权部门数据权限
        /// </summary>
        /// <param name="roleDeptInput"></param>
        /// <returns></returns>
        [HttpPost("/sysrole/grantDept")]
        public async Task<MessageModel<bool>> GrantRoleDept(RoleDeptInput roleDeptInput)
        {
            var role=await _roleService.GetEntityAsync(roleDeptInput.id);
            role!.DataScope =(DataScopeEnum)roleDeptInput.DataScope;
            await _roleService.UpdateAsync(role);
            //这里需要先判断这个用户有没有数据权限
            var res= await _sysRoleDeptService.GrantRoleDept(roleDeptInput);
            var data = new MessageModel<bool>();
            data.result = res;
            return data;
        }
    }
}
