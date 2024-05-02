using Item.DataAccess.Data;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.DataAccess.Repositories.Implementations;

public class StatusRepository(ApplicationDbContext dbContext) 
    : BaseRepository<Status>(dbContext), IStatusRepository
{
}