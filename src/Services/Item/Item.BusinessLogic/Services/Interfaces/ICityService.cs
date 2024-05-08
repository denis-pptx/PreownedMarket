using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models.Entities;

namespace Item.BusinessLogic.Services.Interfaces;

public interface ICityService 
{
    Task<City> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken);
    Task<City> CreateAsync(CityDto cityDto, CancellationToken cancellationToken);
    Task<City> UpdateAsync(Guid id, CityDto cityDto, CancellationToken cancellationToken);
    Task<City> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}