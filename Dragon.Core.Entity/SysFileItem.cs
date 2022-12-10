using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Entity
{
    public class SysFileItem:BaseEntity
    {
        /// <summary>
        /// 文件大小（尺寸为字节）
        /// </summary>
        public long FileSizeInBytes { get;  set; }
        /// <summary>
        /// 用户上传的原始文件名（没有路径）
        /// </summary>
        public string FileName { get;  set; }

        /// <summary>
        /// 两个文件的大小和散列值（SHA256）都相同的概率非常小。因此只要大小和SHA256相同，就认为是相同的文件。
        /// SHA256的碰撞的概率比MD5低很多。
        /// </summary>
        public string? FileSHA256Hash { get;  set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        public string FilePath { get;  set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        public string Suffix { get; set; }
        /// <summary>
        /// 文件版本
        /// </summary>
        public string FileVersion { get; set; }
        /// <summary>
        /// 文件所属部门
        /// </summary>
        public string? FileOwnDept { get; set;}


        public int DeptId { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string? FileType { get; set; }
        /// <summary>
        /// 文件等级
        /// </summary>
        public FileLevelEnum FileLevel { get; set; }
    }
}
