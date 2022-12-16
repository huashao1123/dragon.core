using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class WeChatConfig:BaseEntity
    {

        /// <summary>
        /// 微信公众号唯一标识
        /// </summary>
        public string publicAccount { get; set; }

        /// <summary>
        /// 微信公众号名称
        /// </summary>
        public string publicNick { get; set; }

        /// <summary>
        /// 微信账号
        /// </summary>
        public string weChatAccount { get; set; }

        /// <summary>
        /// 微信名称
        /// </summary>
        public string weChatNick { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 应用秘钥
        /// </summary>
        public string appsecret { get; set; }

        /// <summary>
        /// 公众号推送token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 验证秘钥(验证消息是否真实)
        /// </summary>
        public string interactiveToken { get; set; }

        /// <summary>
        /// 微信公众号token过期时间
        /// </summary>
        public DateTime? tokenExpiration { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
    }
}
