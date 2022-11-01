using Dragon.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IRepository
{
    public interface IUserRoleRepository:IBaseRepository<SysUserRole>
    {
        /// <summary>
        /// 根据用户id得到角色id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<int>> GetRoleIdListAsync(int userId);
    }
}
