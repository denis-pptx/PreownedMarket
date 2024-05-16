using AutoMapper;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.ErrorMessages;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
using Shared.Errors.Exceptions;

namespace Item.BusinessLogic.Services.Implementations;

public class RegionService(
    IUnitOfWork _unitOfWork,
    IRegionRepository _regionRepository, 
    IMapper _mapper) 
    : IRegionService
{
    public async Task<Region> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var region = await _regionRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(region);

        return region;
    }

    public async Task<IEnumerable<Region>> GetAllAsync(CancellationToken cancellationToken)
    {
        var regions = await _regionRepository.GetAllAsync(cancellationToken);

        return regions;
    }


    public async Task<Region> CreateAsync(RegionDto regionDto, CancellationToken cancellationToken)
    {
        var existingRegion = await _regionRepository.GetByNameAsync(regionDto.Name, cancellationToken);

        if (existingRegion is not null)
        {
            throw new ConflictException(RegionErrorMessages.UniqueName);
        }

        var region = _mapper.Map<RegionDto, Region>(regionDto);

        await _regionRepository.AddAsync(region, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return region;
    }

    public async Task<Region> UpdateAsync(Guid id, RegionDto regionDto, CancellationToken cancellationToken)
    {
        var region = await _regionRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(region);

        _mapper.Map(regionDto, region);

        await _regionRepository.UpdateAsync(region, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return region;
    }

    public async Task<Region> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var region = await _regionRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(region);

        await _regionRepository.RemoveAsync(region, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return region;
    }
}