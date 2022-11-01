using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Common
{
    public class CommonConst
    {
        /// <summary>
        /// 默认密码
        /// </summary>
        public const string SysPassword = "aaaa@123";

        /// <summary>
        /// 系统管理员角色编码
        /// </summary>
        public const string SysAdminRoleCode = "SysAdminRoleCode";

        /// <summary>
        /// 演示环境开关配置
        /// </summary>
        public const string SysDemoEnv = "SysDemoEnv";

        /// <summary>
        /// 验证码开关配置
        /// </summary>
        public const string SysCaptchaFlag = "SysCaptchaFlag";

        /// <summary>
        /// 实体所在程序集-代码生成
        /// </summary>
        public static string[] EntityAssemblyName = new string[] { "Dragon.Core", "Dragon.Core.Common" };
    }
}
