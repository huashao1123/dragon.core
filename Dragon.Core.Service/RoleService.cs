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
    public class RoleService : BaseService<SysRole>, IRoleService
    {
        readonly IUser _user;
        readonly IMapper _mapper;
        public RoleService(IBaseRepository<SysRole> baseRepository,IUser user,IMapper mapper) : base(baseRepository)
        {
            _user = user;
            _mapper = mapper;
        }

        public async Task<bool> AddRoleAsync(RoleInput role)
        {
            var sysRole=_mapper.Map<SysRole>(role);
            sysRole.CreatedTime = DateTime.Now;
            sysRole.CreatedName =_user.Name;
            await InsertAsync(sysRole);
            return true;
        }

        public async Task<PageModel<SysRole>> GetPageRoleListAsync(RolePageInput rolePageInput)
        {
            int pageSize=rolePageInput.PageSize;
            int pageIndex= rolePageInput.Page;
            string name = rolePageInput.name;
            int status=rolePageInput.status;
            Expression<Func<SysRole, bool>> e = b => b.IsDrop == false;
            if (!string.IsNullOrEmpty(name) && status > -1)
            {
                e = b => b.IsDrop == false && b.Name.Contains(name) && b.Status == (StateEnum)status;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                e = b => b.IsDrop == false && b.Name.Contains(name);
            }

            if (status > -1)
            {
                e = b => b.IsDrop == false && b.Status == (StateEnum)status;
            }
            PageModel<SysRole> rolePageList= await GetPageEntitiesAsync(e, d => d.Id, pageSize: pageSize, pageIndex: pageIndex);
            return rolePageList;
        }

        public async Task<bool> UpdateRoleAsync(UpdateRoleInput role)
        {
            var sysRole = _mapper.Map<SysRole>(role);
            sysRole.UpdateTime = DateTime.Now;
            sysRole.UpdateName = _user.Name;
            
            int count= await UpdateNotQueryAsync(sysRole,isIgnoreCol: true, properties: new Expression<Func<SysRole, object>>[] { d => d.CreatedName,d=>d.CreatedTime });
            return count > 0;
        }
    }
}
