using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class WeChatUploadFile:BaseEntity
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string UploadFileID { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string UploadFileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int? UploadFileSize { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string UploadFileContentType { get; set; }

        /// <summary>
        /// 文件拓展名
        /// </summary>
        public string UploadFileExtension { get; set; }

        /// <summary>
        /// 文件位置
        /// </summary>
        public string UploadFilePosition { get; set; }

        /// <summary>
        /// 文件上传时间
        /// </summary>
        public DateTime? UploadFileTime { get; set; }

        /// <summary>
        /// 文件备注
        /// </summary>
        public string UploadFileRemark { get; set; }
    }
}
