using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class RoleDeptInput
    {
        public int id { get; set; }

        public int DataScope { get; set; }
        public List<int>? DeptIdList { get; set; }
    }
}
