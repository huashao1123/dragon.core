using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 真实名字
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? avatar { get; set; }
        /// <summary>
        /// 个人简介
        /// </summary>
        public string? Desc { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 角色集合
        /// </summary>
        public List<RoleInfo> roles { get; set; }
    }
}
