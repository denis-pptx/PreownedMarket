using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Library.BLL.Services.Implementations;

namespace Item.BusinessLogic.Services.Implementations;

public class CityService : BaseService<City, CityDto>, ICityService
{
    public CityService(IRepository<City> entityRepository, IMapper mapper) 
        : base(entityRepository, mapper)
    {
    }

    public async override Task<City> CreateAsync(CityDto cityDto, CancellationToken token)
    {
        var existingCity = await _entityRepository.FirstOrDefaultAsync(x => x.Name == cityDto.Name);
        if (existingCity is not null)
        {
            throw new ConflictException("The name of the city must be unique");
        }

        var city = _mapper.Map<CityDto, City>(cityDto);
        var result = await _entityRepository.AddAsync(city, token);

        return result;
    }

    public async override Task<City> UpdateAsync(Guid id, CityDto cityDto, CancellationToken token)
    {
        var city = await _entityRepository.GetByIdAsync(id, token);
        if (city is null)
        {
            throw new NotFoundException($"City is not found");
        }

        _mapper.Map(cityDto, city);
        var result = await _entityRepository.UpdateAsync(city, token);

        return result;
    }
}
