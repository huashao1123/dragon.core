using Dragon.Core.Entity;
using Dragon.Core.Entity.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Linq;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class RoleController : ControllerBase
    {
        readonly IRoleService _roleService;
        readonly IUserRoleService _userRoleService;
        readonly ISysRoleMenuService _sysRoleMenuService;
        readonly ISysRoleDeptService _sysRoleDeptService;
        readonly ISysUserService _sysUserService;
        readonly IUser _user;
        public RoleController(IRoleService roleService, ISysRoleMenuService sysRoleMenuService, ISysRoleDeptService sysRoleDeptService, IUserRoleService userRoleService, ISysUserService sysUserService, IUser user)
        {
            _roleService = roleService;
            _sysRoleMenuService = sysRoleMenuService;
            _sysRoleDeptService = sysRoleDeptService;
            _userRoleService = userRoleService;
            _sysUserService = sysUserService;
            _user = user;
        }
        [HttpGet("/sysrole/pagerolelist")]
        public async Task<MessageModel<PageModel<SysRole>>> GetPageRoleListAsync([FromQuery] RolePageInput rolePageInput)
        {
            var messageModel = new MessageModel<PageModel<SysRole>>();
            messageModel.result= await _roleService.GetPageRoleListAsync(rolePageInput);
            return messageModel;
        }
        [AllowAnonymous]
        [HttpGet("/sysrole/list")]
        public async Task<List<RoleInfo>>GetRoleListAsync()
        {
            // 若非超级管理员则只取拥有角色Id集合
            var roleList = await _roleService.GetListAsync(r => r.IsDrop == false);
            return roleList.Select(r => new RoleInfo { Code = r.Code, Id = r.Id, Name = r.Name }).ToList();
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
        [Transaction]
        [HttpDelete("/sysrole/delete")]
        public async Task<MessageModel<bool>> DeleteRoleAsync([FromBody] int id)
        {
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _roleService.FindAsync(d => d.Id == id);
            if (entity != null)
            {
                if (entity.Code== CommonConst.SysAdminRoleCode)
                {
                    throw new UserFriendlyException("系统管理员禁止删除");
                }
                entity.IsDrop = true;
                await _roleService.UpdateAsync(entity);
                await _sysRoleDeptService.DeleteAsync(d=>d.RoleId==id);
                await _sysRoleMenuService.DeleteAsync(d=>d.RoleId==id);
                await _userRoleService.DeleteAsync(d => d.RoleId == id);
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
                if (entity.Code == CommonConst.SysAdminRoleCode)
                {
                    throw new UserFriendlyException("系统管理员禁止修改");
                }
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
        [Authorize]
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
        [Authorize]
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
            var dataScope= (DataScopeEnum)roleDeptInput.DataScope;
            if (!_user.IsSuperAdmin)
            {
                //这里需要先判断这个用户有没有数据权限操作
                if (dataScope==DataScopeEnum.All)
                {
                    throw new UserFriendlyException("权限不够");
                }

                if (dataScope==DataScopeEnum.Define)
                {
                    var deptList = roleDeptInput.DeptIdList;
                    if (deptList?.Count>0)
                    {
                        var userDeptList = await _sysUserService.GetDeptIdList();
                        if (userDeptList.Count<1)
                        {
                            throw new UserFriendlyException("权限不够");
                        }
                        else if (!userDeptList.All(u=>deptList.Any(d=>d==u)))
                        {
                            throw new UserFriendlyException("权限不够");
                        }

                    }
                    
                }
            }
            var role =await _roleService.GetEntityAsync(roleDeptInput.id);
            role!.DataScope = dataScope;
            await _roleService.UpdateAsync(role);
            var res= await _sysRoleDeptService.GrantRoleDept(roleDeptInput);
            var data = new MessageModel<bool>();
            data.result = res;
            return data;
        }
    }
}
