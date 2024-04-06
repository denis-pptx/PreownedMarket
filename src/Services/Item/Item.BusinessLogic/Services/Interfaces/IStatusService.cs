using Item.DataAccess.Models;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IStatusService 
{
    Task<IEnumerable<Status>> GetAsync(CancellationToken token = default);
    Task<Status> GetByIdAsync(Guid id, CancellationToken token = default);
}