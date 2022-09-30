using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class RoleInfo
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        /// <example>admin</example>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色编码
        /// </summary>
        /// <example>123456</example>
        public string Value { get; set; }
    }
}
