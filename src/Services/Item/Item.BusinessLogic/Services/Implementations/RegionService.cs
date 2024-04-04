using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
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
            throw new ConflictException("The name of the region must be unique");
        }

        var category = _mapper.Map<RegionDto, Region>(regionDto);
        var result = await _entityRepository.AddAsync(category, token);

        return result;
    }

    public async override Task<Region> UpdateAsync(Guid id, RegionDto regionDto, CancellationToken token)
    {
        var region = await _entityRepository.GetByIdAsync(id, token);
        if (region is null)
        {
            throw new NotFoundException($"Region is not found");
        }

        _mapper.Map(regionDto, region);
        var result = await _entityRepository.UpdateAsync(region, token);

        return result;
    }
}
