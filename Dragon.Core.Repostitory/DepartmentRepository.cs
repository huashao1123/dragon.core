using Dragon.Core.Common;
using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Repository
{
    public class DepartMentRepository : BaseRepository<SysDepartMent>, IDepartMentRepository
    {
        public DepartMentRepository(IUintOfWork uintOfWork) : base(uintOfWork)
        {
        }

        public async Task<List<int>> GetChildDeptIdListWithSelfById(int pid)
        {
            var deptTreelist = await Table.ToChildListAsync(r => r.Pid, r => r.Id, pid);
            return deptTreelist.Select(u => u.Id).ToList();
        }

       
    }
}
