using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models.Entities;
using Item.DataAccess.Repositories.Interfaces;

namespace Item.BusinessLogic.Services.Implementations;

public class StatusService(IRepository<Status> _repository) : IStatusService
{
    public async Task<IEnumerable<Status>> GetAsync(CancellationToken token = default)
    {
        return await _repository.GetAsync(token);
    }

    public async Task<Status> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var entity = await _repository.GetByIdAsync(id, token);
        
        NotFoundException.ThrowIfNull(entity);

        return entity;
    }
}