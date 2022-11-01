using Dragon.Core.Common;
using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<int>> GetDeptIdListByDataScope(int dataScope,int deptId)
        {
            var deptIdList=new List<int>();
            if (dataScope== (int)DataScopeEnum.All)
            {
                deptIdList = await Table.Where(d => d.IsDrop == false).Select(d => d.Id).ToListAsync();
            }else if (dataScope== (int)DataScopeEnum.Dept_with_child)
            {
                deptIdList = await GetChildDeptIdListWithSelfById(deptId);
            }else if (dataScope== (int)DataScopeEnum.Dept)
            {
                deptIdList.Add(deptId);
            }

            return deptIdList;
        }


    }
}
