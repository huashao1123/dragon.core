using AutoMapper;
using Dragon.Core.Common;
using Dragon.Core.Common.Helper;
using Dragon.Core.Entity;
using Dragon.Core.Entity.Enum;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly IDepartMentRepository _departMentRepository;
        private readonly IBaseRepository<SysUserDept> _userDepartRepository;
        public DepartmentService(IDepartMentRepository departMentRepository,IUser user,IMapper mapper,IBaseRepository<SysUserDept> userDepartRepository) 
        {
            _user = user;
            _mapper = mapper;
            _departMentRepository = departMentRepository;
            _userDepartRepository = userDepartRepository;
        }

        public async Task<bool> AddDepartment(DepartmentInput departmentInput)
        {
            var dept = await _departMentRepository.GetListAsync(d=>d.Name==departmentInput.name && d.Code==departmentInput.code && d.IsDrop==false);
            if (dept.Count>0)
            {
                throw new UserFriendlyException("已存在相同的部门或编码");
            }
            var entity=_mapper.Map<SysDepartMent>(departmentInput);
            entity.CreatedName = _user.Name;
            entity.CreatedId = Convert.ToInt32(_user.ID);
            entity.CreatedTime = DateTime.Now;
            await _departMentRepository.InsertAsync(entity,true);
            await _userDepartRepository.InsertAsync(new SysUserDept() { DeptId=entity.Id,UserId=Convert.ToInt32(_user.ID)},true);
            return true;
        }

        public async Task<List<DepartmentViewModel>> GetDepartmentList(DepartmentParams departmentParams)
        {
            string? name=departmentParams.Name;
            int status=departmentParams.Status;
            Expression<Func<SysDepartMent, bool>> expression=null;
            if (!string.IsNullOrWhiteSpace(name))
            {
                expression = e => e.Name.Contains(name);
            }

            if (status>-1)
            {
                Expression<Func<SysDepartMent, bool>> expression1 = e => e.Status == (StateEnum)status;
                expression = expression ==null ? expression1: ExpressionHelper.CombineExpressions(expression,expression1);
            }
            Expression<Func<SysDepartMent, bool>> expression2 = e => e.IsDrop == false;
            expression = expression ==null ? expression2 : ExpressionHelper.CombineExpressions(expression,expression2);
            var deptList = await _departMentRepository.GetListAsync(expression);
            var deptViewModeList= _mapper.Map<List<DepartmentViewModel>>(deptList);
            deptViewModeList = deptViewModeList.ToTree((r) => { return r.pid == 0; }, (r, c) => { return r.id == c.pid; }, (r, dataList) =>
            {
                r.children ??= new List<DepartmentViewModel>();
                r.children.AddRange(dataList);
            });
            return deptViewModeList;
        }

        public async Task<bool> UpdateDepartment(UpdateDeptInput departmentInput)
        {
            var dept = await _departMentRepository.GetListAsync(d => d.Name == departmentInput.name && d.Code == departmentInput.code && d.IsDrop == false && d.Id!=departmentInput.id);
            if (dept.Count > 0)
            {
                throw new UserFriendlyException("已存在相同的部门或编码");
            }
            var deptList=await _departMentRepository.GetChildDeptIdListWithSelfById(departmentInput.id);
            if (deptList.Contains(departmentInput.pid))
            {
                throw new UserFriendlyException("当前部门ID不能与父Id相同");
            }
            var updateEntity=_mapper.Map<SysDepartMent>(departmentInput);
            updateEntity.UpdateName = _user.Name;
            updateEntity.UpdateId = Convert.ToInt32(_user.ID);
            updateEntity.UpdateTime = DateTime.Now;
            await _departMentRepository.UpdateAsync(updateEntity,true);
            return true;
        }

        public Task<SysDepartMent?> GetDeptByIdAsync(int id)
        {
            var entity = _departMentRepository.FindAsync(d => d.Id == id);
            return entity;
        }

        public async Task<SysDepartMent?> UpdateAsync(SysDepartMent sysDepartMent)
        {
            var entity = await _departMentRepository.UpdateAsync(sysDepartMent, true);
            return entity;
        }
    }
}
