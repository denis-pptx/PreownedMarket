
using Item.DataAccess.Caching;
using Item.DataAccess.Data;
using Item.DataAccess.Extensions;
using Item.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Item.DataAccess.Repositories.UnitOfWork;

public class UnitOfWork(ApplicationDbContext _dbContext) 
    : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}