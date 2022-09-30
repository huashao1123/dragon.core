using Dragon.Core.Common;
using Dragon.Core.Common.Helper;
using Dragon.Core.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Data
{
    public class DBSeed
    {
        private static string SeedDataFolder = "Data/{0}.tsv";
        public static async Task SeedAsync(EfDbContext customDbContext, string WebRootPath)
        {
            if (string.IsNullOrEmpty(WebRootPath))
            {
                throw new Exception("获取wwwroot路径时，异常！");
            }

            SeedDataFolder = Path.Combine(WebRootPath, SeedDataFolder);
            Console.WriteLine("************ Dragon DataBase Set *****************");
            customDbContext.Database.EnsureCreated();//如果不存在数据库，则创建

            Encoding encoding = new UTF8Encoding(false);
            if (!customDbContext.Set<SysUser>().Any())
            {
                List<SysUser> useInfos = JsonHelper.ParseFormByJson<List<SysUser>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "userInfo"), encoding));
                await customDbContext.Set<SysUser>().AddRangeAsync(useInfos);
            }
            if (!customDbContext.Set<SysRole>().Any())
            {
                List<SysRole> sysRoles = JsonHelper.ParseFormByJson<List<SysRole>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "roleInfo"), encoding));
                await customDbContext.Set<SysRole>().AddRangeAsync(sysRoles);
            }
            if (!customDbContext.Set<SysUserRole>().Any())
            {
                List<SysUserRole> userRoles = JsonConvert.DeserializeObject<List<SysUserRole>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "userrole"), encoding))!;
                await customDbContext.Set<SysUserRole>().AddRangeAsync(userRoles);
            }
            if (!customDbContext.Set<SysMenu>().Any())
            {
                List<SysMenu> sysMenus = JsonConvert.DeserializeObject<List<SysMenu>>(FileHelper.ReadFile(string.Format(SeedDataFolder, "sysmenu"), encoding))!;
                await customDbContext.Set<SysMenu>().AddRangeAsync(sysMenus);
            }
            //Guid depatmentguid = Guid.NewGuid();  //部门Id
            //Guid userId = Guid.NewGuid();  //
            //string userName = "Admin";
            //SysUser sysUser = new SysUser
            //{
            //    CreatedId = userId,
            //    UpdateId = userId,
            //    Id = userId,
            //    DepartmentId = depatmentguid,
            //    Name = userName,
            //    CreatedName = userName,
            //    Password = "123456",
            //    Account = "C-14367"
            //};
            //customDbContext.Set<SysUser>().Add(sysUser);
            await customDbContext.SaveChangesAsync();
        }
    }
}
