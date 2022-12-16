using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class WeChatPushLog:BaseEntity
    {

        /// <summary>
        /// 来自谁
        /// </summary>
        public string PushLogFrom { get; set; }

        /// <summary>
        /// 推送IP
        /// </summary>
        public string PushLogIP { get; set; }

        /// <summary>
        /// 推送客户
        /// </summary>
        public string PushLogCompanyID { get; set; }

        /// <summary>
        /// 推送用户
        /// </summary>
        public string PushLogToUserID { get; set; }

        /// <summary>
        /// 推送模板ID
        /// </summary>
        public string PushLogTemplateID { get; set; }

        /// <summary>
        /// 推送内容
        /// </summary>
        public string PushLogContent { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime? PushLogTime { get; set; }

        /// <summary>
        /// 推送状态(Y/N)
        /// </summary>
        public string PushLogStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string PushLogRemark { get; set; }

        /// <summary>
        /// 推送OpenID
        /// </summary>
        public string PushLogOpenid { get; set; }

        /// <summary>
        /// 推送微信公众号
        /// </summary>
        public string PushLogPublicAccount { get; set; }
    }
}
