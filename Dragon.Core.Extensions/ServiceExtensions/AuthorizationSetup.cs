using Dragon.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// 授权服务
    /// </summary>
    public static class AuthorizationSetup
    {
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            //Tips：注释中的中括号【xxx】的内容，与下边region中的模块，是一一匹配的

            /*
             * 如果不想走数据库，仅仅想在代码里配置授权，这里可以按照下边的步骤：
             * 1步、【1、基于角色的API授权】
             * 很简单，只需要在指定的接口上，配置特性即可，比如：[Authorize(Roles = "Admin,System,Others")]
             * 
             * 但是如果你感觉"Admin,System,Others"，这样的字符串太长的话，可以把这个融合到简单策略里          
             * 具体的配置，看下文的Region模块【2、基于策略的授权（简单版）】 ，然后在接口上，配置特性：[Authorize(Policy = "A_S_O")]
             * 
             * 
             * 2步、配置Bearer认证服务，具体代码看下文的 region 【第二步：配置认证服务】
             * 
             * 3步、开启中间件
             */



            /*
             * 如果想要把权限配置到数据库，步骤如下：
             * 1步、【3复杂策略授权】
             * 具体的查看下边 region 内的内容
             * 
             * 2步、配置Bearer认证服务，具体代码看下文的 region 【第二步：配置认证服务】
             * 
             * 3步、开启中间件
             */
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            //    options.AddPolicy("A_S_O", policy => policy.RequireRole("Admin", "System", "Others"));
            //});


            //3、综上所述，设置权限，必须要三步走，授权 + 配置认证服务 + 开启授权中间件，只不过自定义的中间件不能验证过期时间，所以我都是用官方的。
            #region JTW参数配置
            string symetrickeyAsBase64 = Appsettings.app(new string[] { "JwtSettings", "SecretKey" });//签名的key，需要大于16位。
            var keyByteArray = Encoding.ASCII.GetBytes(symetrickeyAsBase64);
            var signKey = new SymmetricSecurityKey(keyByteArray); //加密签名key
            var issuer = Appsettings.app(new string[] { "JwtSettings", "Issuer" });  //发行人
            var audience = Appsettings.app(new string[] { "JwtSettings", "Audience" }); //听众
            var signingCredentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

            // 需要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<PermissionItem>();

            // 角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,//角色菜单实体
                ClaimTypes.Role,//基于角色的授权
                issuer,//发行人
                audience,//听众
                signingCredentials,//签名认证凭据
                expiration: TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                );
            #endregion
            // 3、自定义复杂的策略授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.Name,
                         policy => policy.Requirements.Add(permissionRequirement));
            });

            // 注入权限处理器
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
        }
    }
}
