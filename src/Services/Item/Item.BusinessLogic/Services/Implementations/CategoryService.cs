using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Library.BLL.Services.Implementations;

namespace Item.BusinessLogic.Services.Implementations;

public class CategoryService : BaseService<Category, CategoryDto>, ICategoryService
{
    public CategoryService(IRepository<Category> entityRepository, IMapper mapper) 
        : base(entityRepository, mapper)
    {
        
    }

    public async override Task<Category> CreateAsync(CategoryDto categoryDto, CancellationToken token)
    {
        var existingCategory = await _entityRepository.FirstOrDefaultAsync(x => x.Name == categoryDto.Name);
        if (existingCategory is not null)
        {
            throw new ConflictException(CategoryErrorMessages.UniqueName);
        }

        var category = _mapper.Map<CategoryDto, Category>(categoryDto);
        var result = await _entityRepository.AddAsync(category, token);

        return result;
    }

    public async override Task<Category> UpdateAsync(Guid id, CategoryDto categoryDto, CancellationToken token)
    {
        var category = await _entityRepository.GetByIdAsync(id, token);
        if (category is null)
        {
            throw new NotFoundException(GenericErrorMessages<Category>.NotFound);
        }

        _mapper.Map(categoryDto, category);
        var result = await _entityRepository.UpdateAsync(category, token);

        return result;
    }
}
