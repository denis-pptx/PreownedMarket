using Item.DataAccess.Models.Entities;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IStatusService 
{
    Task<IEnumerable<Status>> GetAsync(CancellationToken token = default);
    Task<Status> GetByIdAsync(Guid id, CancellationToken token = default);
}