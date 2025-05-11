using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.ErrorMessages;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
using Shared.Errors.Exceptions;

namespace Item.BusinessLogic.Services.Implementations;

public class RegionService(
    IUnitOfWork unitOfWork,
    IRegionRepository regionRepository) 
    : IRegionService
{
    public async Task<Region> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var region = await regionRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(region);

        return region;
    }

    public async Task<IEnumerable<Region>> GetAllAsync(CancellationToken cancellationToken)
    {
        var regions = await regionRepository.GetAllAsync(cancellationToken);

        return regions;
    }


    public async Task<Region> CreateAsync(RegionDto regionDto, CancellationToken cancellationToken)
    {
        var existingRegion = await regionRepository.GetByNameAsync(regionDto.Name, cancellationToken);

        if (existingRegion is not null)
        {
            throw new ConflictException(RegionErrorMessages.UniqueName);
        }

        var region = new Region
        {
            Name = regionDto.Name,
        };
        
        await regionRepository.AddAsync(region, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return region;
    }

    public async Task<Region> UpdateAsync(Guid id, RegionDto regionDto, CancellationToken cancellationToken)
    {
        var region = await regionRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(region);

        region.Name = regionDto.Name;

        await regionRepository.UpdateAsync(region, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return region;
    }

    public async Task<Region> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var region = await regionRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(region);

        await regionRepository.RemoveAsync(region, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return region;
    }
}