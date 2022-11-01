using Dragon.Core.Common;

namespace Dragon.Core.IService
{
    public interface ISysUserService:IBaseService<SysUser>
    {
        Task<PageModel<UserViewModel>> GetPageUserListAsynce(UserPageInput userPageInput);
        Task<bool> AddUserAsynce(UserInput userInput);

        Task<bool> UpdateUserAsync(UserInput userInput);

        Task<bool> GrantUserDeptAsync(UserDeptInput userDeptInput);

        Task<List<int>> OwnDeptIdListAsync(int userId);

        Task CheckDataScope(int deptId);
        Task DeleteDeptByUserId(int userId);

        Task<List<int>> GetDeptIdList();
    }
}
