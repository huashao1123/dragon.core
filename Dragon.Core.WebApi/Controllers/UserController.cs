using Dragon.Core.Data.Migrations;
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
        readonly IUser _user;
        public UserController(ISysUserService userService, IUserRoleService userRoleService, IUser user)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _user = user;
        }
        [HttpGet("/sysuser/pagelist")]
        public async Task<MessageModel<PageModel<UserViewModel>>> GetUserPageListAsync([FromQuery] UserPageInput userPageInput)
        {
            var userPageList=await _userService.GetPageUserListAsynce(userPageInput);
            MessageModel<PageModel<UserViewModel>> messageModel = new MessageModel<PageModel<UserViewModel>>();
            messageModel.result = userPageList;
            return messageModel;
        }

        [HttpPost("/sysuser/add")]
        public async Task<MessageModel<bool>> AddSysUserAsync(UserInput userInput)
        {

            await CheckDataScope(userInput.DepartmentId);
            var res=await _userService.AddUserAsynce(userInput);
            var data=new MessageModel<bool>();
            data.result = res;
            return data;
        }

        [HttpPut("/sysuser/update")]
        public async Task<MessageModel<bool>>UpdateSysUserAsync(UserInput userInput)
        {
            await CheckDataScope(userInput.DepartmentId);
            var res = await _userService.UpdateUserAsync(userInput);
            var data = new MessageModel<bool>();
            data.result = res;
            return data;
        }
        [Transaction]
        [HttpDelete("/sysuser/delete/{id}")]
        public async Task<MessageModel<bool>>DeleteSysUserAsync(int id)
        {
            if (id==Convert.ToInt32(_user.ID))
            {
                throw new UserFriendlyException("非法操作，禁止删除本尊");
            }
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _userService.FindAsync(d => d.Id == id);
            if (entity != null)
            {
                await CheckDataScope(entity.DepartmentId);
                if (entity.UserType==UserTypeEnum.SuperAdmin)
                {
                    throw new UserFriendlyException("超级管理员账户禁止删除");
                }
                entity.IsDrop = true;
                await _userService.UpdateAsync(entity);
                data.result = true;
                await _userRoleService.DeleteAsync(d => d.UserId == id);//删除此账号关联角色

                await _userService.DeleteDeptByUserId(id);  //删除此账号关联数据部门
            }
            return data;
        }

        [HttpPost("/sysuser/setstatus")]
        public async Task<MessageModel<bool>>SetUserStatusAsync(UserStatus userStatus)
        {
            int id = userStatus.Id;
            if (id == Convert.ToInt32(_user.ID))
            {
                throw new UserFriendlyException("非法操作，禁止修改本尊状态");
            }

            if (!Enum.IsDefined(typeof(StateEnum), userStatus.Status))
            {
                throw new UserFriendlyException("非法操作，错误的状态");
            }
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _userService.FindAsync(d => d.Id == id);
            if (entity != null)
            {
                await CheckDataScope(entity.DepartmentId);
                if (entity.UserType == UserTypeEnum.SuperAdmin)
                {
                    throw new UserFriendlyException("超级管理员账户禁止修改状态");
                }
                entity.Status = (StateEnum)userStatus.Status;
                await _userService.UpdateAsync(entity);
                data.result = true;
            }
            return data;
        }

        [HttpPost("/sysuser/grantrole")]
        public async Task<MessageModel<bool>> GrantUserRoleAsync(UserRoleInput userRoleInput)
        {
            var user=await _userService.GetEntityAsync(userRoleInput.Id);
            if (user!=null && user.UserType==UserTypeEnum.SuperAdmin)
            {
                throw new UserFriendlyException("超级管理员账户禁止分配角色");
            }
            await CheckDataScope(userRoleInput.DeptId); //这里其实要进行数据范围检查，比如超出这个用户使用数据的权限
            bool res=await _userRoleService.GrantRole(userRoleInput);
            MessageModel<bool> data = new MessageModel<bool>();
            data.result = res;
            return data;
        }

        [HttpPost("/sysuser/grantdept")]
        public async Task<MessageModel<bool>> GrantUserDept(UserDeptInput userDeptInput)
        {
            //这里其实要进行数据范围检查，比如超出这个用户使用数据的权限
            await CheckDataScope(userDeptInput.DeptId);
            bool res = await _userService.GrantUserDeptAsync(userDeptInput);
            MessageModel<bool> data = new MessageModel<bool>();
            data.result = res;
            return data;
        }

        [HttpGet("/sysuser/ownrolelist")]
        public async Task<MessageModel<List<int>>>GetOwnUserRoleListAsync(int userId)
        {
            var roleIdList = await _userRoleService.GetRoleId(userId);
            var data=new MessageModel<List<int>>();
            data.result= roleIdList;
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
        /// <summary>
        /// 检查是否有部门权限
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        private async Task CheckDataScope(int deptId)
        {
            await _userService.CheckDataScope(deptId); //检查当前用户是否有此部门的权限
        }
    }
}
