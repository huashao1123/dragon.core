using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class DepartmentParams
    {
        public string? Name { get; set; }
        public int Status { get; set; } = -1;
    }

    public class DepartmentInput
    {
        public int id { get; set; }
        public string name { get; set; }

        public string? code { get; set; }
        public string? remark { get; set; }
        public int status { get; set; }

        public int order { get; set; }
        public int pid { get; set; }
    }

    public class UpdateDeptInput : DepartmentInput
    {

    }

    public class DepartmentViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? code { get; set; }

        public string? remark { get; set; }

        public int status { get; set; }

        public int order { get; set; }

        public int pid { get; set; }

        public List<DepartmentViewModel> children { get; set; }
    }
}
