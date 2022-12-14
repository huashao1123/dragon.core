using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string? Work { get; set; }
    }
}
