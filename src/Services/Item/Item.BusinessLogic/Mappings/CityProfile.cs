using AutoMapper;
using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models;

namespace Item.BusinessLogic.Mappings;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<CityDto, City>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
    }
}
