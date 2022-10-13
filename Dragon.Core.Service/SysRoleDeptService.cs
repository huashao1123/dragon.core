using Dragon.Core.Entity;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class SysRoleDeptService : BaseService<SysRoleDept>, ISysRoleDeptService
    {
        public SysRoleDeptService(IBaseRepository<SysRoleDept> baseRepository) : base(baseRepository)
        {

        }

        public async Task<List<int>> GetDeptIdList(int roleId)
        {
            List<int> deptIdList = (await GetListAsync(d => d.RoleId == roleId)).Select(d => d.DeptId).ToList();
            return deptIdList;
        }

        public async Task<bool> GrantRoleDept(RoleDeptInput roleDeptInput)
        {
            await DeleteAsync(d=>d.RoleId==roleDeptInput.id);
            if (roleDeptInput.DataScope==(int)DataScopeEnum.Define)
            {
                var roleDepts = roleDeptInput.DeptIdList.Select(u => new SysRoleDept
                {
                    RoleId = roleDeptInput.id,
                    DeptId = u
                }).ToList();
                await InsertManyAsync(roleDepts);
            }
            return true;
        }
    }
}
