using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IRegionService 
    : IBaseService<Region, RegionDto>
{
}