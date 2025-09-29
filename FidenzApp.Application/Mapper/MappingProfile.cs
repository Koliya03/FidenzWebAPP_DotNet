using AutoMapper;
using FidenzApp.Application.DTO;
using FidenzApp.Domain.Entities;

namespace FidenzApp.Application.Mapper
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            CreateMap<Address,AddressDto>();
            CreateMap<Customers, CustomerDto>();
            CreateMap<Customers, CustomerDto>()
            .ForMember(d => d.tags,
                o => o.MapFrom(s => s.tags ?? new List<string>()));

            CreateMap<IGrouping<int, Customers>, ZipGroupDto>()
            .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.Key))
            .ForMember(d => d.Customers, o => o.MapFrom(s => s.ToList()));
        }
    }
}
