using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.Implementations;

public class CategoryRepository(ApplicationDbContext dbContext) 
    : BaseRepository<Category>(dbContext), ICategoryRepository
{
    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _entities
            .Where(category => category.Name == name)
            .SingleOrDefaultAsync(cancellationToken);
    }
}