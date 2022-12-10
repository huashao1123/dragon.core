using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public enum FileLevelEnum
    {
        /// <summary>
        /// 公开文件
        /// </summary>
        [Description("公开文件")]
        PublicFile =1,

        /// <summary>
        /// 受控文件
        /// </summary>
        [Description("受控文件")]
        ControlledFile = 2,

        /// <summary>
        /// 机密文件
        /// </summary>
        [Description("机密文件")]
        ConfidentialFile = 3,

        /// <summary>
        /// 绝密文件
        /// </summary>
        [Description("绝密文件")]
        TopSecretFile = 4,
    }
}
