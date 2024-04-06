using AutoMapper;
using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models;

namespace Item.BusinessLogic.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.NormalizedName))
            .ReverseMap();
    }
}
