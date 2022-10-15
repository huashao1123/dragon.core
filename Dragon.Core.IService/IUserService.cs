using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IService
{
    public interface IUserService:IBaseService<SysUser>
    {
        Task<PageModel<UserViewModel>> GetPageUserListAsynce(UserPageInput userPageInput);
        Task<bool> AddUserAsynce(UserInput userInput);

        Task<bool> UpdateUserAsync(UserInput userInput);
    }
}
