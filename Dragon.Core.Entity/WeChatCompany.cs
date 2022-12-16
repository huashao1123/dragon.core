using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class WeChatCompany:BaseEntity
    {
        /// <summary>
        /// 公司ID
        /// </summary> 
        public string CompanyID { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>

        public string CompanyName { get; set; }
        /// <summary>
        /// 公司IP
        /// </summary>
        public string CompanyIP { get; set; }
        /// <summary>
        /// 公司备注
        /// </summary>
        public string CompanyRemark { get; set; }
        /// <summary>
        /// api地址
        /// </summary>
        public string CompanyAPI { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
    }
}
