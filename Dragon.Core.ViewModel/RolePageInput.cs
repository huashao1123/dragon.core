using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class RolePageInput:BasePageInput
    {
        public string? name { get; set; }
        public int status { get; set; } = -1;
    }
}
