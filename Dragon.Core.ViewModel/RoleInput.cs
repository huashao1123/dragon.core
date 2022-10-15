using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class RoleInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int Status { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public  int Order { get; set; }

        /// <summary>
        /// 数据范围类型
        /// </summary>
        public int DataScope { get; set; } = 4;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

    }

    public class UpdateRoleInput:RoleInput
    {

    }
}
