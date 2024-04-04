using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models;

namespace Item.BusinessLogic.Services.Interfaces;

public interface ICityService 
    : IBaseService<City, CityDto>
{
}
