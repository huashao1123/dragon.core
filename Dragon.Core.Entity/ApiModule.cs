using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    /// <summary>
    /// api接口模块
    /// </summary>
    public class ApiModule : BaseEntity
    {
        /// <summary>
        /// API接口名
        /// </summary>
        public string ApiName { get; set; }
        /// <summary>
        /// api接口地址
        /// </summary>
        public string ApiUrl { get; set; }
    }
}
