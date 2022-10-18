using Dragon.Core.Entity.Enum;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class UserController : ControllerBase
    {
        readonly ISysUserService _userService;
        readonly IUserRoleService _userRoleService;
        public UserController(ISysUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }
        [HttpGet("/sysuser/pagelist")]
        public async Task<MessageModel<PageModel<UserViewModel>>> GetUserPageListAsync(UserPageInput userPageInput)
        {
            var userPageList=await _userService.GetPageUserListAsynce(userPageInput);
            MessageModel<PageModel<UserViewModel>> messageModel = new MessageModel<PageModel<UserViewModel>>();
            messageModel.result = userPageList;
            return messageModel;
        }

        [HttpPost("/sysuser/add")]
        public async Task<MessageModel<bool>> AddSysUserAsync(UserInput userInput)
        {
            var res=await _userService.AddUserAsynce(userInput);
            var data=new MessageModel<bool>();
            data.result = res;
            return data;
        }

        [HttpPut("/sysuser/update")]
        public async Task<MessageModel<bool>>UpdateSysUserAsync(UserInput userInput)
        {
            var res = await _userService.UpdateUserAsync(userInput);
            var data = new MessageModel<bool>();
            data.result = res;
            return data;
        }

        [HttpDelete("/sysuser/delete/{id}")]
        public async Task<MessageModel<bool>>DeleteSysUserAsync(int id)
        {
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _userService.FindAsync(d => d.Id == id);
            if (entity != null)
            {
                entity.IsDrop = true;
                await _userService.UpdateAsync(entity);
                data.result = true;
            }
            return data;
        }

        [HttpPost("/sysuser/setstatus")]
        public async Task<MessageModel<bool>>SetUserStatusAsync(int id,int status)
        {
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _userService.FindAsync(d => d.Id == id);
            if (entity != null)
            {
                entity.Status = (StateEnum)status;
                await _userService.UpdateAsync(entity);
                data.result = true;
            }
            return data;
        }

        [HttpPost("/sysuser/grantrole")]
        public async Task<MessageModel<bool>> GrantUserRoleAsync(UserRoleInput userRoleInput)
        {
            //这里其实要进行数据范围检查，比如超出这个用户使用数据的权限
           bool res=await _userRoleService.GrantRole(userRoleInput);
            MessageModel<bool> data = new MessageModel<bool>();
            data.result = res;
            return data;
        }

        [HttpPost("/sysuser/grantdept")]
        public async Task<MessageModel<bool>> GrantUserDept(UserDeptInput userDeptInput)
        {
            //这里其实要进行数据范围检查，比如超出这个用户使用数据的权限

            bool res = await _userService.GrantUserDeptAsync(userDeptInput);
            MessageModel<bool> data = new MessageModel<bool>();
            data.result = res;
            return data;
        }

        [HttpGet("/sysuser/ownrolelist")]
        public async Task<MessageModel<List<RoleInfo>>>GetOwnUserRoleListAsync(int userId)
        {
            var roleInfoList = await _userRoleService.GetRoleInfoList(userId);
            var data=new MessageModel<List<RoleInfo>>();
            data.result=roleInfoList;
            return data;
        }

        [HttpGet("/sysuser/owndeptidlist")]
        public async Task<MessageModel<List<int>>>GetOwnUserDeptList(int userId)
        {
            var data=new MessageModel<List<int>>();
            var res =await _userService.OwnDeptIdListAsync(userId);
            data.result = res;
            return data;
        }
    }
}
