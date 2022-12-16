using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class WeChatSub:BaseEntity
    {
        public string SubFromPublicAccount { get; set; }

        /// <summary>
        /// 绑定公司id
        /// </summary>
        public string CompanyID { get; set; }

        /// <summary>
        /// 绑定员工id
        /// </summary>
        public string SubJobID { get; set; }

        /// <summary>
        /// 绑定微信id
        /// </summary>
        public string SubUserOpenID { get; set; }

        /// <summary>
        /// 绑定微信联合id
        /// </summary>
        public string SubUserUnionID { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        public DateTime SubUserRegTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? SubUserRefTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string SubUserRemark { get; set; }

        /// <summary>
        /// 是否已解绑
        /// </summary>
        public bool IsUnBind { get; set; }

        /// <summary>
        /// 上次绑定微信id
        /// </summary>
        public string LastSubUserOpenID { get; set; }
    }
}
