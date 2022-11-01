using System.Linq.Expressions;

namespace Dragon.Core.IService
{
    public interface IUserRoleService
    {
        /// <summary>
        /// 得到角色名，以,分割
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<string> GetUserRoleNames(int userId);
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<RoleInfo>> GetRoleInfoList(int userId);
        /// <summary>
        /// 根据用户id得到角色id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<int>>GetRoleId(int userId);

        public Task<bool> GrantRole(UserRoleInput userRoleInput);

        public Task DeleteAsync(Expression<Func<SysUserRole, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default);
    }
}
