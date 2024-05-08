using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models.Entities;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IRegionService
{
    Task<Region> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Region>> GetAllAsync(CancellationToken cancellationToken);
    Task<Region> CreateAsync(RegionDto regionDto, CancellationToken cancellationToken);
    Task<Region> UpdateAsync(Guid id, RegionDto regionDto, CancellationToken cancellationToken);
    Task<Region> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}