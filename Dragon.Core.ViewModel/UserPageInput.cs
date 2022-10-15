using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class UserPageInput:BasePageInput
    {
        public string? name { get; set; }
        public string? Mobile { get; set; }

        public int DepartmentId { get; set; } = 0;
    }
}
