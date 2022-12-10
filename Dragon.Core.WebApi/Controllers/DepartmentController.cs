using Dragon.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ISysUserService _sysUserService;
        private readonly IUser _user;
        public DepartmentController(IDepartmentService departmentService, ISysUserService sysUserService, IUser user)
        {
            _departmentService = departmentService;
            _sysUserService = sysUserService;
            _user = user;
        }

        [HttpGet("/dept/list")]
        public async Task<MessageModel<List<DepartmentViewModel>>> GetDeptList([FromQuery] DepartmentParams departmentParams)
        {
            var deptList=await _departmentService.GetDepartmentList(departmentParams);
            MessageModel<List<DepartmentViewModel>> messageModel = new MessageModel<List<DepartmentViewModel>>();
            messageModel.result=deptList;
            return messageModel;
        }

        [HttpPost("/dept/add")]
        public async Task<MessageModel<bool>>AddDept(DepartmentInput departmentInput)
        {
            if (!_user.IsSuperAdmin)
            {
                int pid = departmentInput.pid;
                if (pid == 0)
                {
                    throw new UserFriendlyException("没有权限添加机构");
                }else
                {
                    var deptList =await _sysUserService.GetDeptIdList();
                    if (deptList.Count<1 || !deptList.Contains(pid))
                    {
                        throw new UserFriendlyException("没有权限添加机构");
                    }
                }
            }
            bool success = await _departmentService.AddDepartment(departmentInput);
            MessageModel<bool> messageModel = new MessageModel<bool>();
            messageModel.result=success;
            return messageModel;
        }

        [HttpPut("/dept/update")]
        public async Task<MessageModel<bool>>UpdateDept(UpdateDeptInput updateDeptInput)
        {
            int pid=updateDeptInput.pid;
            if (pid != 0)
            {
               var dept= await _departmentService.GetDeptByIdAsync(pid);
                _ = dept ?? throw new UserFriendlyException("机构不存在");
            }
            if (!_user.IsSuperAdmin)
            {
                var deptList = await _sysUserService.GetDeptIdList();
                if (deptList.Count < 1 || !deptList.Contains(updateDeptInput.id))
                {
                    throw new UserFriendlyException("没有权限修改机构");
                }
            }
            bool success=await _departmentService.UpdateDepartment(updateDeptInput);
            MessageModel<bool> messageModel = new MessageModel<bool>();
            messageModel.result = success;
            return messageModel;
        }
        [HttpDelete("/dept/delete")]
        public async Task<MessageModel<bool>>DeleteDept([FromBody] int id)
        {
            if (!_user.IsSuperAdmin)
            {
                var deptList = await _sysUserService.GetDeptIdList();
                if (deptList.Count < 1 || !deptList.Contains(id))
                {
                    throw new UserFriendlyException("没有权限删除机构");
                }
            }
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _departmentService.GetDeptByIdAsync(id);
            if (entity != null)
            {
                entity.IsDrop = true;
                await _departmentService.UpdateAsync(entity);
                data.result = true;
            }
            return data;
        }
    }
}
