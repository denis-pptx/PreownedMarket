using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models.Entities;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IRegionService 
    : IBaseService<Region, RegionDto>
{
}