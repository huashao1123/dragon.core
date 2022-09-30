using Autofac;
using Dragon.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    /// <summary>
    /// autofac容器注入类
    /// </summary>
    public class AutofacModuleRegister : Autofac.Module
    {
        private static ILifetimeScope _container;

        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;
            #region 带有接口层的服务注入,解耦
            var servicesDllFile = Path.Combine(basePath, "Dragon.Core.Service.dll");
            var repositoryDllFile = Path.Combine(basePath, "Dragon.Core.Repository.dll");

            if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            {
                Exception ex = new Exception("Repository.dll和service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。");
                LogServer.WriteErrorLog("容器注入", nameof(AutofacModuleRegister), ex);
                throw ex;
            }
            // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。
            //var cacheType = new List<Type>();
            //if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<BlogRedisCacheAOP>();
            //    cacheType.Add(typeof(BlogRedisCacheAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<BlogCacheAOP>();
            //    cacheType.Add(typeof(BlogCacheAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<BlogTranAOP>();
            //    cacheType.Add(typeof(BlogTranAOP));
            //}
            //if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
            //{
            //    builder.RegisterType<BlogLogAOP>();
            //    cacheType.Add(typeof(BlogLogAOP));
            //}
            //// 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                     .AsImplementedInterfaces()
                     .InstancePerDependency()
                     .PropertiesAutowired();//允许属性注入
            //                                //.EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
            //                                //.InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。
            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                   .AsImplementedInterfaces()
                   .InstancePerDependency()
                   .PropertiesAutowired();//允许属性注入

            var repositoryTypes = assemblysRepository.GetTypes().Where(t => t.IsGenericType && t.IsClass && !String.IsNullOrEmpty(t.Namespace) && t.Name.Contains("Repository"));

            foreach (var repositoryType in repositoryTypes)
            {
                Type interRepositoryType = repositoryType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault();

                if (interRepositoryType != null)
                {
                    builder.RegisterGeneric(repositoryType).As(interRepositoryType).InstancePerLifetimeScope();   //注入泛型仓储，如果不单独注入，则会报错
                }
            }

            #endregion
            //#region 带有接口层的服务注入,没有解耦
            //var assemblysServices = Assembly.Load("DragonSea.Service");//要记得!!!这个注入的是实现类层，不是接口层！不是 IServices
            //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces().InstancePerDependency();//指定已扫描程序集中的类型注册为提供所有其实现的接口。
            //var assemblysRepository = Assembly.Load("DragonSea.Repository");//模式是 Load(解决方案名)
            //builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces().InstancePerDependency();


            //var repositoryTypes = assemblysRepository.GetTypes().Where(t => t.IsGenericType && t.IsClass && !String.IsNullOrEmpty(t.Namespace) && t.Name.Contains("Repository"));

            //foreach (var repositoryType in repositoryTypes)
            //{
            //    Type interRepositoryType = repositoryType.GetTypeInfo().ImplementedInterfaces.FirstOrDefault();
            //    if (interRepositoryType != null)
            //    {
            //        builder.RegisterGeneric(repositoryType).As(interRepositoryType).InstancePerLifetimeScope();   //注入泛型仓储，如果不单独注入，则会报错
            //    }
            //}
            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)) .InstancePerLifetimeScope();//取消通过指定类型注入，通过采用上面的反射解析注入

            //#endregion


            #region 没有接口层的服务层注入

            //因为没有接口层，所以不能实现解耦，只能用 Load 方法。
            //注意如果使用没有接口的服务，并想对其使用 AOP 拦截，就必须设置为虚方法
            //var assemblysServicesNoInterfaces = Assembly.Load("Blog.Core.Services");
            //builder.RegisterAssemblyTypes(assemblysServicesNoInterfaces);

            #endregion

            #region 没有接口的单独类 class 注入

            //只能注入该类中的虚方法
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Person)));
            //.EnableClassInterceptors();
            //.InterceptedBy(cacheType.ToArray());

            #endregion

            //单个注册
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().PropertiesAutowired();
            // 手动高亮
            builder.RegisterBuildCallback(container => _container = container);
        }

        public static ILifetimeScope GetContainer()
        {
            return _container;
        }
    }
}
