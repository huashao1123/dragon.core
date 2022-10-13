using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.ViewModel
{
    public class RoleMenuInPut
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单id的集合
        /// </summary>
        public List<int> MenuIdList { get; set; }
    }
}
