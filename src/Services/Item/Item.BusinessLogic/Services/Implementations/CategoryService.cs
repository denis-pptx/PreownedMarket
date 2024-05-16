﻿using AutoMapper;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.ErrorMessages;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.UnitOfWork;
using Shared.Errors.Exceptions;

namespace Item.BusinessLogic.Services.Implementations;

public class CategoryService(
    IUnitOfWork _unitOfWork,
    ICategoryRepository _categoryRepository, 
    IMapper _mapper) 
    : ICategoryService
{
    public async Task<Category> CreateAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(categoryDto.Name, cancellationToken);

        if (existingCategory is not null)
        {
            throw new ConflictException(CategoryErrorMessages.UniqueName);
        }

        var category = _mapper.Map<CategoryDto, Category>(categoryDto);

        await _categoryRepository.AddAsync(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<Category> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(category);

        await _categoryRepository.RemoveAsync(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);

        return categories;
    }

    public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        NotFoundException.ThrowIfNull(category);

        return category;
    }

    public async Task<Category> UpdateAsync(Guid id, CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        NotFoundException.ThrowIfNull(category);

        _mapper.Map(categoryDto, category);

        await _categoryRepository.UpdateAsync(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }
}