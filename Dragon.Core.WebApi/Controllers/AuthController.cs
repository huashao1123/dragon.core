using Dragon.Core.Data.Migrations;
using Dragon.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dragon.Core.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiCpntroller
    {
        private readonly ISysUserService _sysUserService;
        private readonly IUserRoleService _userRoleService;
        private readonly PermissionRequirement _requirement;
        private readonly IUser _user;

        public AuthController(ISysUserService sysUserService, IUserRoleService userRoleService, PermissionRequirement requirement, IUser user)
        {
            _sysUserService = sysUserService;
            _userRoleService = userRoleService;
            _requirement = requirement;
            _user = user;
        }
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<MessageModel<TokenInfoViewModel>>Login(LoginInput loginInput)
        {
            string userName = loginInput.UserName;
            string password = MD5Util.GetMD5_32(loginInput.Password);
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return Failed<TokenInfoViewModel>("用户名或者密码不能为空");
            }
            password = MD5Util.GetMD5_32(loginInput.Password);
            var user = await _sysUserService.FindAsync(d => d.Name == userName && d.Password == password);
            if (user != null)
            {
                var userRoles = await _userRoleService.GetUserRoleNames(user.Id);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim>
                {
                    new Claim(ClaimConst.SuperAdmin,user.UserType.ToString()),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
                };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                return Success(token, "获取成功");
            }
            else
            {
                return Failed<TokenInfoViewModel>("认证失败");
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("/getUserInfo")]
        [Authorize]
        public async Task<MessageModel<UserInfoModel>> GetUserInfo()
        {
            int id = Convert.ToInt32(_user.ID);
            var data = new MessageModel<UserInfoModel>();
            var user = await _sysUserService.FindAsync(d=>d.Id==id);
            var roleList = await _userRoleService.GetRoleInfoList(id);
            UserInfoModel userInfoModel = new UserInfoModel
            {
                avatar = user?.Avater,
                UserId = _user.ID,
                Username = _user.Name,
                RealName = user!.Account,
                Desc = user.JobName,
                roles = roleList.Select(r => new LoginRole { RoleName = r.Name,Value=r.Code }).ToList(),
            };
            data.result = userInfoModel;
            return data;
        }
        [HttpGet("/logout")]
        [Authorize]
        public async Task<MessageModel<string>>Logout()
        {
           MessageModel<string> messageModel = new MessageModel<string>();
            messageModel.result = "";
            //可以记录日志
            return messageModel;
        }
    }
}
