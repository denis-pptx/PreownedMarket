using AutoMapper;
using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models;

namespace Item.BusinessLogic.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<RegionDto, Region>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
    }
}
