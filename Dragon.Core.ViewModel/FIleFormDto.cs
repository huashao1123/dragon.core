using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class FileFormDto
    {
        public int id { get; set; }
        public string fileVersion { get; set; }

        public int fileLevel { get; set; }

        public List<IFormFile>? File { get; set; }
    }

}
