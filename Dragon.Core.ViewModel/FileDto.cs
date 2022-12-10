using Dragon.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class BaseFileDto
    {
        public string fileVersion { get; set; }

        public FileLevelEnum fileLevel { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime updateTime { get; set; }= DateTime.Now;
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdateName { get; set; }

        public string FileOwnDept { get; set; }

        public int DeptId { get; set; }
    }

    public class FileDto:BaseFileDto
    { 
        public long FileSizeInBytes { get; set; }
        public string FileName { get; set; }

        public string Suffix { get; set; }

        public string FilePath { get; set; }
    }

    public class FileOutput
    {
        public int id { get; set; }
        public string FileName { get; set; }
        public string FileOwnDept { get; set; }
        public string Suffix { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FileVersion { get; set; }
        public FileLevelEnum FileLevel { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateName { get; set; }
    }
}
