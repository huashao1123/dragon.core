﻿using AutoMapper;
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
    public class DepartmentService : BaseService<SysDepartMent>, IDepartmentService
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;
        public DepartmentService(IBaseRepository<SysDepartMent> baseRepository,IUser user,IMapper mapper) : base(baseRepository)
        {
            _user = user;
            _mapper = mapper;
        }

        public async Task<bool> AddDepartment(DepartmentInput departmentInput)
        {
            var entity=_mapper.Map<SysDepartMent>(departmentInput);
            entity.CreatedName = _user.Name;
            entity.CreatedId = Convert.ToInt32(_user.ID);
            entity.CreatedTime = DateTime.Now;
            await InsertAsync(entity);
            return true;
        }

        public async Task<List<DepartmentViewModel>> GetDepartmentList(DepartmentParams departmentParams)
        {
            string name=departmentParams.Name;
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
            var deptList = await GetListAsync(expression);
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
            var updateEntity=_mapper.Map<SysDepartMent>(departmentInput);
            updateEntity.UpdateName = _user.Name;
            updateEntity.UpdateId = Convert.ToInt32(_user.ID);
            updateEntity.UpdateTime = DateTime.Now;
            await UpdateAsync(updateEntity);
            return true;
        }
    }
}