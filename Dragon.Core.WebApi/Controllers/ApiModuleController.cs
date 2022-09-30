using Dragon.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Permissions.Name)]
    public class ApiModuleController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IApiModuleService _apiModuleService;
        public ApiModuleController(IUser user, IApiModuleService apiModuleService)
        {
            _user = user;
            _apiModuleService = apiModuleService;
        }
        /// <summary>
        /// 获取api列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("/getapilist")]
        public async Task<MessageModel<List<ApiModuleViewModel>>> GetApiList(string? name,int status=-1)
        {
            var data = new MessageModel<List<ApiModuleViewModel>>();
            var result = await _apiModuleService.GetApiModuleListAsync(name, status);
            data.result = result;
            return data;
        }
        /// <summary>
        /// 增加api接口模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/addapimodule")]
        public async Task<MessageModel<bool>> AddApiMouleAsync(ApiModuleViewModel model)
        {
            var data = new MessageModel<bool>();
            model.createdTime = DateTime.Now;
            model.createdName = _user.Name;
            var result = await _apiModuleService.AddApiModuleAsync(model);
            data.result = result;
            return data;
        }
        /// <summary>
        /// 更新api接口模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("/updateapimodule")]
        public async Task<MessageModel<bool>> UpdateApiMouleAsync(ApiModuleViewModel model)
        {
            var data = new MessageModel<bool>();
            var result = await _apiModuleService.UpdateApiModuleAsync(model);
            data.result = result;
            return data;
        }
        /// <summary>
        /// 删除api接口模块，最好软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/deleteapimodule")]
        //[Route("{id}")]
        public async Task<MessageModel<bool>> DeleteApiMouleAsync(int id)
        {
            var data = new MessageModel<bool>();
            data.result= false;
            ApiModule apiModule = (await _apiModuleService.GetEntityAsync(id))!;
            if (apiModule != null)
            {
                apiModule.IsDrop = true;
                await _apiModuleService.UpdateAsync(apiModule);
                data.result = true;
            }
            return data;
        }
    }
}
