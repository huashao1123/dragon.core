using Dragon.Core.Common;
using Dragon.Core.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly IUser _user;
        private readonly ISysRoleMenuService _sysRoleMenuService;
        public PermissionHandler(IAuthenticationSchemeProvider schemeProvider,IHttpContextAccessor accessor,IUser user,ISysRoleMenuService sysRoleMenuService)
        {
            _schemes = schemeProvider;
            _accessor = accessor;
            _user = user;
            _sysRoleMenuService = sysRoleMenuService;
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
            if (httpContext != null)
            {
                var questUrl = httpContext.Request.Path.Value.ToLower();

                // 整体结构类似认证中间件UseAuthentication的逻辑，具体查看开源地址
                // https://github.com/dotnet/aspnetcore/blob/master/src/Security/Authentication/Core/src/AuthenticationMiddleware.cs
                httpContext.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
                {
                    OriginalPath = httpContext.Request.Path,
                    OriginalPathBase = httpContext.Request.PathBase
                });

                // Give any IAuthenticationRequestHandler schemes a chance to handle the request
                // 主要作用是: 判断当前是否需要进行远程验证，如果是就进行远程验证
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await _schemes.GetRequestHandlerSchemesAsync())
                {
                    if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        context.Fail();
                        return;
                    }
                }
                //判断请求是否拥有凭据，即有没有登录，没有登录的时候身份为空
                var defaultAuthenticate = await _schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    //用户未登录
                    if (result.Principal == null)
                    {
                        context.Fail(new AuthorizationFailureReason(this, "授权已过期,请重新授权"));
                        return;
                    }
                    httpContext.User = result.Principal;
                    //token是否过期了
                    bool isExp = (httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) != null && DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) >= DateTime.Now;
                    if (!isExp)
                    {
                        context.Fail(new AuthorizationFailureReason(this, "授权已过期,请重新授权"));
                        return;
                    }
                    //超级用户拥有所有接口权限
                    if (_user.IsSuperAdmin)
                    {
                        context.Succeed(requirement);
                        return;
                    }
                    ///从用户认证声明中拿到角色，但是如果这个用户角色刚好被人修改了的话，可能会有问题，这样的话就必须每次从数据库或者缓存中获取
                    var currentUserRoles = (from item in httpContext.User.Claims
                                            where item.Type == requirement.ClaimType
                                            select item.Value).ToList();
                    //获取系统中所有的角色和菜单的关系集合
                    if (!requirement.Permissions.Any())
                    {
                        var data = await _sysRoleMenuService.GetApiUrlsAsync();
                        requirement.Permissions= data.Select(d=> new PermissionItem { Role=d.roleName,Url=d.apiurl}).ToList();
                    }
                    var isMatchRole = false;
                    var permisssionRoles = requirement.Permissions.Where(w => currentUserRoles.Contains(w.Role));
                    foreach (var item in permisssionRoles)
                    {
                        try
                        {
                            if (Regex.Match(questUrl, item.Url?.ObjToString().ToLower())?.Value == questUrl)
                            {
                                isMatchRole = true;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }

                    //验证权限
                    if (currentUserRoles.Count <= 0 || !isMatchRole)
                    {
                        context.Fail();
                        return;
                    }
                    context.Succeed(requirement);
                }
                //判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
                if (!(questUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST") || !httpContext.Request.HasFormContentType)))
                {
                    context.Fail();
                    return;
                }
            }
           
        }
    }
}
