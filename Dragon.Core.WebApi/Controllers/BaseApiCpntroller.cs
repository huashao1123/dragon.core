using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Controllers
{
    [ApiController]
    public class BaseApiCpntroller : ControllerBase
    {
        [NonAction]
        public MessageModel<T> Success<T>(T data, string msg = "成功")
        {
            return new MessageModel<T>()
            {
                message = msg,
                result = data,
            };
        }
        [NonAction]
        public MessageModel<T> Failed<T>(string msg = "失败", int status = 500)
        {
            return new MessageModel<T>()
            {
                code = status,
                message = msg,
                result = default,
            };
        }
    }
}
