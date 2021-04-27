using AutoMapper;
using CMS.Elements.Api.Web.Host.Models.Common;
using CMS.Elements.Api.Contracts.Entities;
using System.Collections.Generic;

namespace CMS.Elements.Api.Web.Host.Utilities
{
    public static class AutoMapConverter
    {
        private static readonly IMapper mapper;
        static AutoMapConverter()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Contact , ContactViewModel>(MemberList.None);
                cfg.CreateMap< ContactViewModel, Contact >(MemberList.None);
                cfg.CreateMap<ResponseData, ResponseDataViewModel>(MemberList.None);
            });
            mapper = config.CreateMapper();
        }

        public static TDestinationObj ConvertObject<TSourceObj, TDestinationObj>(TSourceObj srcObj)
        {
            return mapper.Map<TSourceObj, TDestinationObj>(srcObj);
        }

        public static List<TDestinationObj> ConvertObjectCollection<TSourceObj, TDestinationObj>(List<TSourceObj> srcObjList)
        {
            return mapper.Map<List<TSourceObj>, List<TDestinationObj>>(srcObjList);
        }
    }   
}