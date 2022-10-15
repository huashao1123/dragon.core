using AutoMapper;
using Dragon.Core.Common;
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
    public class UserService : BaseService<SysUser>, IUserService
    {
        readonly IUser _user;
        readonly IMapper _mapper;
        public UserService(IBaseRepository<SysUser> baseRepository,IUser user,IMapper mapper) : base(baseRepository)
        {
            _user = user;
            _mapper=mapper;
        }

        public Task<bool> AddUserAsynce(UserInput userInput)
        {
            throw new NotImplementedException();
        }

        public Task<PageModel<UserViewModel>> GetPageUserListAsynce(UserPageInput userPageInput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserInput userInput)
        {
            throw new NotImplementedException();
        }
    }
}
