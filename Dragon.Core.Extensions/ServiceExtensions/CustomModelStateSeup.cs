using Dragon.Core.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions.ServiceExtensions
{
    public static class CustomModelStateSeup
    {
        /// <summary>
        /// 设置自定义模型验证失败，返回统一的格式消息
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddCustomModelStateSetup(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    // 获取失败的验证信息列表
                    var errors = actionContext.ModelState
                        .Where(s => s.Value != null && s.Value.ValidationState == ModelValidationState.Invalid)
                        .SelectMany(s => s.Value!.Errors.ToList())
                        .Select(e => e.ErrorMessage)
                        .ToArray();

                    // 统一返回格式
                    var result = new MessageModel()
                    {
                        code = StatusCodes.Status400BadRequest,
                        message = "数据验证不通过！",
                        type = "failed",
                        result = errors
                    };

                    // 设置结果
                    return new BadRequestObjectResult(result);
                };
            });
        }
    }
}
