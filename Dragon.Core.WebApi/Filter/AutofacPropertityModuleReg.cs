using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace Dragon.Core.WebApi.Filter
{
    public class AutofacPropertityModuleReg : Autofac.Module
    {
        /// <summary>
        /// 允许控制器属性注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();

        }
    }
}
