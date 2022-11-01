﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Dragon.Core.Common.Helper;

namespace Dragon.Core.WebApi.Filter
{
    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        //private readonly IHubContext<ChatHub> _hubContext;
        private readonly ILogger<GlobalExceptionsFilter> _loggerHelper;

        public GlobalExceptionsFilter(IWebHostEnvironment env, ILogger<GlobalExceptionsFilter> loggerHelper)
        {
            _env = env;
            _loggerHelper = loggerHelper;
            // _hubContext = hubContext;
        }

        public void OnException(ExceptionContext context)
        {
            var json = new MessageModel<string>();

            json.message = context.Exception.Message;//错误信息
            json.code = 500;//500异常 
           
            //var errorAudit = "Unable to resolve service for";
            //if (!string.IsNullOrEmpty(json.msg) && json.msg.Contains(errorAudit))
            //{
            //    json.msg = json.msg.Replace(errorAudit, $"（若新添加服务，需要重新编译项目）{errorAudit}");
            //}
            if (context.Exception is UserFriendlyException)
            {
                json.code = 299;
                json.result = context.Exception.Message;
            }
            if (_env.EnvironmentName.ObjToString().Equals("Development"))
            {
                //json.msgDev = context.Exception.StackTrace;//堆栈信息
            }
            var res = new ContentResult();
            res.Content = JsonHelper.GetJSON<MessageModel<string>>(json);

            context.Result = res;

            //MiniProfiler.Current.CustomTiming("Errors：", json.msg);




            //_hubContext.Clients.All.SendAsync("ReceiveUpdate", LogLock.GetLogData()).Wait();

        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }

    }
}
