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
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
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
            bool success = await _departmentService.AddDepartment(departmentInput);
            MessageModel<bool> messageModel = new MessageModel<bool>();
            messageModel.result=success;
            return messageModel;
        }

        [HttpPut("/dept/update")]
        public async Task<MessageModel<bool>>UpdateDept(UpdateDeptInput updateDeptInput)
        {
            bool success=await _departmentService.UpdateDepartment(updateDeptInput);
            MessageModel<bool> messageModel = new MessageModel<bool>();
            messageModel.result = success;
            return messageModel;
        }
        [HttpDelete("/dept/delete/{id}")]
        public async Task<MessageModel<bool>>DeleteDept(int id)
        {
            var data = new MessageModel<bool>();
            data.result = false;
            var entity = await _departmentService.FindAsync(d => d.Id == id);
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
