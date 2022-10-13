using AutoMapper;
using Dragon.Core.Common;
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
    public class ApiModuleService : BaseService<ApiModule>, IApiModuleService
    {
        readonly IMapper _mapper;
        readonly IUser _user;
        public ApiModuleService(IBaseRepository<ApiModule> baseRepository,IMapper mapper,IUser user) : base(baseRepository)
        {
            _mapper = mapper;
            _user = user;
        }

        public async Task<bool> AddApiModuleAsync(ApiModuleViewModel model)
        {
            ApiModule apiModule = _mapper.Map<ApiModule>(model);
            var result = await InsertAsync(apiModule);
            if (result!=null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ApiModuleViewModel>> GetApiModuleListAsync(string? name, int status)
        {
            Expression<Func<ApiModule, bool>> e = b => b.IsDrop == false;
            if (!string.IsNullOrEmpty(name) && status > -1)
            {
                e = b => b.IsDrop == false && b.ApiName.Contains(name) && b.Status == (StateEnum)status;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                e = b => b.IsDrop == false && b.ApiName.Contains(name);
            }

            if (status > -1)
            {
                e = b => b.IsDrop == false && b.Status == (StateEnum)status;
            }

            var apiList = await GetListAsync(e);
            List<ApiModuleViewModel> result = _mapper.Map<List<ApiModuleViewModel>>(apiList);
            return result;
        }

        public async Task<bool> UpdateApiModuleAsync(ApiModuleViewModel model)
        {
            ApiModule apiModule = _mapper.Map<ApiModule>(model);

            Expression<Func<ApiModule, object>> expression = b => b.CreatedName;
            var result=await UpdateNotQueryAsync(apiModule,isIgnoreCol:true, properties: expression);

           
            return result>0;
        }
    }
}
