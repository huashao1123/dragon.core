using Dragon.Core.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// JWT权限 认证服务
    /// </summary>
    public static class AuthenticationSetup
    {
        public static void AddAuthenticationSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            string symetrickeyAsBase64 = Appsettings.app(new string[] { "JwtSettings", "SecretKey" });//签名的key，需要大于16位。
            var keyByteArray = Encoding.ASCII.GetBytes(symetrickeyAsBase64);
            var signKey = new SymmetricSecurityKey(keyByteArray); //加密签名key
            var issuer = Appsettings.app(new string[] { "JwtSettings", "Issuer" });  //发行人
            var audience = Appsettings.app(new string[] { "JwtSettings", "Audience" }); //听众
            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,//发行人
                ValidateAudience = true,
                ValidAudience = audience,//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };

            // 开启Bearer认证
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                //o.DefaultForbidScheme = nameof(ApiResponseHandler);
            })
             // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnChallenge = context =>
                     {
                         context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                         return Task.CompletedTask;
                     },
                     OnAuthenticationFailed = context =>
                     {
                         var jwtHandler = new JwtSecurityTokenHandler();
                         var token = context.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "");

                         if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
                         {
                             var jwtToken = jwtHandler.ReadJwtToken(token);

                             if (jwtToken.Issuer != issuer)
                             {
                                 context.Response.Headers.Add("Token-Error-Iss", "issuer is wrong!");
                             }

                             if (jwtToken.Audiences.FirstOrDefault() != audience)
                             {
                                 context.Response.Headers.Add("Token-Error-Aud", "Audience is wrong!");
                             }
                         }


                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                     //给SignalR 赋值给集线器的链接管道添加Token验证
                     //OnMessageReceived = context =>
                     //{
                     //    var accessToken = context.Request.Query["access_token"];
                     //    var path = context.HttpContext.Request.Path;
                     //    if (!string.IsNullOrEmpty(path) && path.StartsWithSegments("/api2/chatHub"))
                     //    {
                     //        context.Token = accessToken;
                     //    }
                     //    return Task.CompletedTask;
                     //}
                 };
             });
            //.AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });
        }
    }
}
