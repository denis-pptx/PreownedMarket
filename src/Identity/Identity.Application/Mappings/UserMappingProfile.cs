using AutoMapper;
using Identity.Application.Features.Identity.Commands.RegisterUser;
using Identity.Domain.Models;

namespace Identity.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserCommand, User>();
    }
}
