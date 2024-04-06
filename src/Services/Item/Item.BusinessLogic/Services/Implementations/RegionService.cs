using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Implementations;
using Library.BLL.Services.Implementations;

namespace Item.BusinessLogic.Services.Implementations;

public class RegionService : BaseService<Region, RegionDto>, IRegionService
{
    public RegionService(IRepository<Region> entityRepository, IMapper mapper) 
        : base(entityRepository, mapper)
    {
    }

    public async override Task<Region> CreateAsync(RegionDto regionDto, CancellationToken token)
    {
        var existingRegion = await _entityRepository.FirstOrDefaultAsync(x => x.Name == regionDto.Name);

        if (existingRegion is not null)
        {
            throw new ConflictException(RegionErrorMessages.UniqueName);
        }

        var region = _mapper.Map<RegionDto, Region>(regionDto);
        var result = await _entityRepository.AddAsync(region, token);

        return result;
    }

    public async override Task<Region> UpdateAsync(Guid id, RegionDto regionDto, CancellationToken token)
    {
        var region = await _entityRepository.GetByIdAsync(id, token);

        if (region is null)
        {
            throw new NotFoundException(GenericErrorMessages<Region>.NotFound);
        }

        _mapper.Map(regionDto, region);
        var result = await _entityRepository.UpdateAsync(region, token);

        return result;
    }

    public async override Task<IEnumerable<Region>> GetAsync(CancellationToken token)
    {
        var specification = new RegionWithCitiesSpecification();
        var entities = await _entityRepository.GetAsync(specification, token);

        return entities;
    }

    public async override Task<Region> GetByIdAsync(Guid id, CancellationToken token)
    {
        var specification = new RegionWithCitiesSpecification(id);
        var entity = await _entityRepository.FirstOrDefaultAsync(specification, token);

        if (entity is null)
        {
            throw new NotFoundException(GenericErrorMessages<Region>.NotFound);
        }

        return entity;
    }
}