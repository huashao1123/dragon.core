using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    /// <summary>
    /// 接口viewmodel
    /// </summary>
    public class ApiModuleViewModel
    {
        public int id { get; set; }
        /// <summary>
        /// API接口名
        /// </summary>
        public string apiName { get; set; }
        /// <summary>
        /// api接口地址
        /// </summary>
        public string apiUrl { get; set; }

        public DateTime createdTime { get; set; }

        public string? createdName { get; set; }

        public string? remark { get; set; }
        public int status { get; set; }

        public int order { get; set; }
    }
}
