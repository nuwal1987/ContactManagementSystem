using AutoMapper;
using CMS.Elements.Api.Contracts.Entities;
using System.Collections.Generic;

namespace CMS.Elements.Api.Dal.RA.Models.Utilities
{
    public static class TypeMapperConverter
    {
        private static readonly IMapper mapper;

        static TypeMapperConverter()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Contact , ContactInformation>(MemberList.None);
                cfg.CreateMap<ContactInformation, Contact >(MemberList.None);
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