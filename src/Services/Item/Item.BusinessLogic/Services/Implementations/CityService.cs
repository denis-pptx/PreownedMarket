using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.ErrorMessages;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
using Shared.Errors.Exceptions;

namespace Item.BusinessLogic.Services.Implementations;

public class CityService(
    IUnitOfWork unitOfWork,
    ICityRepository cityRepository) 
    : ICityService
{
    public async Task<City> CreateAsync(CityDto cityDto, CancellationToken cancellationToken)
    {
        var existingCity = await cityRepository.GetByNameAsync(cityDto.Name, cancellationToken);

        if (existingCity is not null)
        {
            throw new ConflictException(CityErrorMessages.UniqueName);
        }

        var city = new City
        {
            Name = cityDto.Name
        };
        
        await cityRepository.AddAsync(city, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return city;
    }

    public async Task<City> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var city = await cityRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(city);

        await cityRepository.RemoveAsync(city, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return city;
    }

    public async Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken)
    {
        var cities = await cityRepository.GetAllAsync(cancellationToken);

        return cities;
    }

    public async Task<City> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var city = await cityRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(city);

        return city;
    }

    public async Task<City> UpdateAsync(Guid id, CityDto cityDto, CancellationToken cancellationToken)
    {
        var city = await cityRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(city);

        city.Name = cityDto.Name;

        await cityRepository.UpdateAsync(city, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return city;
    }
}