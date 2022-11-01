using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dragon.Core.WebApi.Filter
{
    /// <summary>
    /// 增加统一返回格式
    /// </summary>
    public class ApiResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor)
            {
                ControllerActionDescriptor controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                var array= controllerActionDescriptor.MethodInfo.GetCustomAttributes(true).OfType<IgnoreApiResultAttribute>().ToArray();
                if (array.Any())
                {
                    var customAttribute = array.First();
                    if ((bool)customAttribute.ignore)
                    {
                        base.OnActionExecuted(context);
                        return;
                    }
                }
            }

            if (context.Result != null)
            {
                if (context.Result is ObjectResult)
                {
                    var result = (ObjectResult)context.Result;
                    int statusCode = result.StatusCode ?? 200;


                    if ((bool)(result?.Value.GetType().Name.Contains(nameof(MessageModel))))
                    {
                        context.Result = new ObjectResult(result.Value) { StatusCode = statusCode};
                    }
                    else
                    {
                        context.Result = new ObjectResult(new MessageModel() { result = result.Value, code = statusCode }) { StatusCode = statusCode, };
                    }
                }
                else if (context.Result is EmptyResult)
                {
                    context.Result = new ObjectResult(new MessageModel() { message = "",result="" });
                }
                else if (context.Result is ContentResult)
                {
                    context.Result = new ObjectResult(new MessageModel { result = (context.Result! as ContentResult).Content });
                }
                else if (context.Result is StatusCodeResult)
                {
                    //context.Result = new ObjectResult();
                }
                else if (context.Result is Exception)
                {

                }
                else if (context.Result is MessageModel message)
                {
                    return;
                }

            }
            else
            {
                context.Result = new ObjectResult(new MessageModel() { message = "" });
            }
            base.OnActionExecuted(context);
        }

    }
}
