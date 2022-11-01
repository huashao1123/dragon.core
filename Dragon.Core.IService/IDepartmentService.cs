using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IService
{
    public interface IDepartmentService
    {
        public Task<List<DepartmentViewModel>>GetDepartmentList(DepartmentParams departmentParams);

        public Task<bool> AddDepartment(DepartmentInput departmentInput);

        public Task<bool> UpdateDepartment(UpdateDeptInput departmentInput);

        Task<SysDepartMent?> GetDeptByIdAsync(int id);

        Task<SysDepartMent?> UpdateAsync(SysDepartMent sysDepartMent);

    }
}
