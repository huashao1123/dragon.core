using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class UserViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? account { get; set; }

        public string? avatar { get; set; }

        public bool sex { get; set; }

        public string? email { get; set; }

        public string mobile { get; set; }

        public int status { get; set; }

        public string? remark { get; set; }

        public DateTime createdTime { get; set; }=DateTime.Now;

        public int DepartmentId { get; set; }

        public int jobStatus { get; set; }
    }

    public class UserInput: UserViewModel
    {

    }

    public class UserRoleInput
    {
        public int Id { get; set; }
        public int DeptId { get; set; }

        public List<int>? RoleIdList { get; set; }
    }

    public class UserDeptInput
    {
        public int Id { get; set; }
        public List<int>? DeptIdList { get; set; }
    }
}
