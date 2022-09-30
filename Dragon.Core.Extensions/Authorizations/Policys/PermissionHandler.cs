using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// 权限授权处理器
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider _schemes { get; set; }
        private readonly IHttpContextAccessor _accessor;
        public PermissionHandler(IAuthenticationSchemeProvider schemeProvider,IHttpContextAccessor accessor)
        {
            _schemes = schemeProvider;
            _accessor = accessor;
        }
        /// <summary>
        /// 自定义权限处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var httpContext = _accessor.HttpContext;
            //判断请求是否拥有凭据，即有没有登录，没有登录的时候身份为空
            var defaultAuthenticate = await _schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate!=null)
            {
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                if (result.Principal==null)
                {
                    context.Fail();
                    return;
                }
            }
            context.Succeed(requirement);
        }
    }
}
