using AutoMapper;
using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models;

namespace Item.BusinessLogic.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
    }
}
