using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models.Entities;

namespace Item.BusinessLogic.Services.Interfaces;

public interface ICategoryService 
{
    Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<Category> CreateAsync(CategoryDto categoryDto, CancellationToken cancellationToken);
    Task<Category> UpdateAsync(Guid id, CategoryDto categoryDto, CancellationToken cancellationToken);
    Task<Category> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}