using Dragon.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IRepository
{
    public interface IDepartMentRepository:IBaseRepository<SysDepartMent>
    {
        Task<List<int>> GetChildDeptIdListWithSelfById(int pid);
    }
}
