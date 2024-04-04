using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Library.BLL.Services.Implementations;

namespace Item.BusinessLogic.Services.Implementations;

public class CategoryService : BaseService<Region, RegionDto>, ICategoryService
{
    public CategoryService(IRepository<Region> entityRepository, IMapper mapper) 
        : base(entityRepository, mapper)
    {
        
    }

    public async override Task<Region> CreateAsync(RegionDto categoryDto, CancellationToken token)
    {
        var existingCategory = await _entityRepository.FirstOrDefaultAsync(x => x.Name == categoryDto.Name);
        if (existingCategory is not null)
        {
            throw new ConflictException("The name of the category must be unique");
        }

        var category = _mapper.Map<RegionDto, Region>(categoryDto);
        var result = await _entityRepository.AddAsync(category, token);

        return result;
    }

    public async override Task<Region> UpdateAsync(Guid id, RegionDto categoryDto, CancellationToken token)
    {
        var category = await _entityRepository.GetByIdAsync(id, token);
        if (category is null)
        {
            throw new NotFoundException($"Category is not found");
        }

        _mapper.Map(categoryDto, category);
        var result = await _entityRepository.UpdateAsync(category, token);

        return result;
    }
}
