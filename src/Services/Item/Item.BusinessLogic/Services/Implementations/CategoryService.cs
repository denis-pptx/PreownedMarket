using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.ErrorMessages;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
using Shared.Errors.Exceptions;

namespace Item.BusinessLogic.Services.Implementations;

public class CategoryService(
    IUnitOfWork unitOfWork,
    ICategoryRepository categoryRepository) 
    : ICategoryService
{
    public async Task<Category> CreateAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var existingCategory = await categoryRepository.GetByNameAsync(categoryDto.Name, cancellationToken);

        if (existingCategory is not null)
        {
            throw new ConflictException(CategoryErrorMessages.UniqueName);
        }

        var category = new Category
        {
            Name = categoryDto.Name,
            NormalizedName = categoryDto.NormalizedName
        };
        
        await categoryRepository.AddAsync(category, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<Category> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(category);

        await categoryRepository.RemoveAsync(category, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(cancellationToken);

        return categories;
    }

    public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(category);

        return category;
    }

    public async Task<Category> UpdateAsync(Guid id, CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(category);

        category.Name = categoryDto.Name;
        category.NormalizedName = categoryDto.NormalizedName;

        await categoryRepository.UpdateAsync(category, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }
}