using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IService
{
    public interface IApiModuleService:IBaseService<ApiModule>
    {
        /// <summary>
        /// 获取接口列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<List<ApiModuleViewModel>> GetApiModuleListAsync(string? name, int status);
        /// <summary>
        /// 增加接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> AddApiModuleAsync(ApiModuleViewModel model);
        /// <summary>
        /// 更新接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateApiModuleAsync(ApiModuleViewModel model);
    }
}
