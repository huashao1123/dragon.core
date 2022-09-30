using AutoMapper;
using Dragon.Core.Entity;
using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Extensions
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<ApiModule, ApiModuleViewModel>().ReverseMap();
            CreateMap<SysMenu, MenuTreeViewModel>()
               .ForPath(d => d.meta.title, s => s.MapFrom(t => t.Title))
               .ForPath(d => d.meta.icon, s => s.MapFrom(t => t.Icon))
               .ForPath(d => d.meta.orderNo, s => s.MapFrom(t => t.Order))
               .ForPath(d => d.meta.frameSrc, s => s.MapFrom(t => t.FrameSrc))
               .ForPath(d => d.meta.ignoreKeepAlive, s => s.MapFrom(t => t.IgnoreKeepAlive))
               .ForPath(d => d.meta.hideMenu, s => s.MapFrom(t => t.HideMenu))
               .ForPath(d => d.meta.currentActiveMenu, s => s.MapFrom(t => t.CurrentActiveMenu));
            CreateMap<SysMenu, MenuListItem>().ForPath(d => d.pid, s => s.MapFrom(t => t.ParId));
            CreateMap<MenuInput, SysMenu>().ForPath(d => d.ParId, s => s.MapFrom(t => t.pid));
            CreateMap<SysDepartMent, DepartmentViewModel>().ForPath(d => d.pid, s => s.MapFrom(t => t.Pid));
            CreateMap<DepartmentInput, SysDepartMent>();
            CreateMap<UpdateDeptInput, SysDepartMent>();
        }
    }
}
