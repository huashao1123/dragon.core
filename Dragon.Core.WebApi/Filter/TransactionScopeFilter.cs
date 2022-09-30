using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Transactions;

namespace Dragon.Core.WebApi.Filter
{
    /// <summary>
	/// 事务过滤器
	/// </summary>
    public class TransactionScopeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool transactionalAttribute = false;
            if (context.ActionDescriptor is ControllerActionDescriptor)
            {
                var actionDesc = (ControllerActionDescriptor)context.ActionDescriptor;
                transactionalAttribute = actionDesc.MethodInfo
                    .IsDefined(typeof(TransactionalAttribute));//当方法上面有这个事务过滤器，就采用事务
            }
            if (!transactionalAttribute)
            {
                await next();
                return;
            }
            using var txScope =
                    new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var result = await next();
            if (result.Exception == null)
            {
                txScope.Complete();
            }

        }
    }
}
