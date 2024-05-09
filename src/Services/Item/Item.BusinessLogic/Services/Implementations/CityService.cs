using AutoMapper;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.ErrorMessages;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Repositories.UnitOfWork;
using Shared.Errors.Exceptions;

namespace Item.BusinessLogic.Services.Implementations;

public class CityService(
    IUnitOfWork _unitOfWork,
    ICityRepository _cityRepository, 
    IMapper _mapper) 
    : ICityService
{
    public async Task<City> CreateAsync(CityDto cityDto, CancellationToken cancellationToken)
    {
        var existingCity = await _cityRepository.GetByNameAsync(cityDto.Name, cancellationToken);

        if (existingCity is not null)
        {
            throw new ConflictException(CityErrorMessages.UniqueName);
        }

        var city = _mapper.Map<CityDto, City>(cityDto);

        await _cityRepository.AddAsync(city, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return city;
    }

    public async Task<City> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(city);

        await _cityRepository.RemoveAsync(city, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return city;
    }

    public async Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.GetAllAsync(cancellationToken);

        return cities;
    }

    public async Task<City> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(city);

        return city;
    }

    public async Task<City> UpdateAsync(Guid id, CityDto cityDto, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(city);

        _mapper.Map(cityDto, city);

        await _cityRepository.UpdateAsync(city, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return city;
    }
}