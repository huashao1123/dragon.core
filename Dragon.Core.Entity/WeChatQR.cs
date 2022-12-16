using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class WeChatQR:BaseEntity
    {

        /// <summary>
        /// 主键id,ticket
        /// </summary>
        public string QRticket { get; set; }

        /// <summary>
        /// 需要绑定的公司
        /// </summary>
        public string QRbindCompanyID { get; set; }

        /// <summary>
        /// 需要绑定的员工id
        /// </summary>
        public string QRbindJobID { get; set; }
        /// <summary>
        /// 需要绑定的员工昵称
        /// </summary>
        public string QRbindJobNick { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime QRcrateTime { get; set; }

        /// <summary>
        /// 关联的公众号
        /// </summary>
        public string QRpublicAccount { get; set; }

        /// <summary>
        /// 是否已使用
        /// </summary>
        public bool QRisUsed { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? QRuseTime { get; set; }

        /// <summary>
        /// 关联的微信用户id
        /// </summary>
        public string QRuseOpenid { get; set; }
    }
}
