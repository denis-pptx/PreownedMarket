using AutoMapper;
using Item.BusinessLogic.Models.DTOs;

namespace Item.BusinessLogic.Mappings;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        CreateMap<ItemDto, DataAccess.Models.Item>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
    }
}