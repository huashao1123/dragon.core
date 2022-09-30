using Dragon.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    // <summary>
    /// 中间件
    /// 记录请求和响应数据
    /// </summary>
    public class RequRespLogMilddleware
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;
        private readonly ILogger<RequRespLogMilddleware> _logger;
        public RequRespLogMilddleware(RequestDelegate next, ILogger<RequRespLogMilddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (Appsettings.app("Middleware", "RequestResponseLog", "Enabled").ObjToBool())
            {
                // 过滤，只有接口
                if (context.Request.Path.Value.Contains("api"))
                {
                    context.Request.EnableBuffering();
                    Stream originalBody = context.Response.Body;

                    try
                    {
                        // 存储请求数据
                        await RequestDataLog(context);

                        using (var ms = new MemoryStream())
                        {
                            context.Response.Body = ms;

                            await _next(context);

                            // 存储响应数据
                            ResponseDataLog(context.Response, ms);

                            ms.Position = 0;
                            await ms.CopyToAsync(originalBody);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 记录异常                        
                        _logger.LogError(ex.Message + "" + ex.InnerException);
                    }
                    finally
                    {
                        context.Response.Body = originalBody;
                    }
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task RequestDataLog(HttpContext context)
        {
            var request = context.Request;
            var sr = new StreamReader(request.Body);

            var content = $" QueryData:{request.Path + request.QueryString}\r\n BodyData:{await sr.ReadToEndAsync()}";

            if (!string.IsNullOrEmpty(content))
            {
                //Parallel.For(0, 1, e =>
                //{
                //    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Request Data:", content });

                //});
                LogServer.WriteLog("RequestResponseLog", "Request Data:", content);

                request.Body.Position = 0;
            }
        }

        private void ResponseDataLog(HttpResponse response, MemoryStream ms)
        {
            ms.Position = 0;
            var ResponseBody = new StreamReader(ms).ReadToEnd();

            // 去除 Html
            var reg = "<[^>]+>";
            var isHtml = Regex.IsMatch(ResponseBody, reg);

            if (!string.IsNullOrEmpty(ResponseBody))
            {
                //Parallel.For(0, 1, e =>
                //{
                //    LogLock.OutSql2Log("RequestResponseLog", new string[] { "Response Data:", ResponseBody });

                //});
                LogServer.WriteLog("RequestResponseLog", "Response Data:", ResponseBody);
            }
        }
    }
}
