using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;
using Shared.Errors.Exceptions;

namespace Item.BusinessLogic.Services.Implementations;

public class StatusService(IStatusRepository _statusRepository) 
    : IStatusService
{
    public async Task<IEnumerable<Status>> GetAsync(CancellationToken token = default)
    {
        return await _statusRepository.GetAllAsync(token);
    }

    public async Task<Status> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var status = await _statusRepository.GetByIdAsync(id, token);
        
        NotFoundException.ThrowIfNull(status);

        return status;
    }
}