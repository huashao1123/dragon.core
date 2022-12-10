using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class FilePageInput:BasePageInput
    {
        public string? FileOwnDept { get; set; }
        public string? FileName { get; set; }
    }
}
