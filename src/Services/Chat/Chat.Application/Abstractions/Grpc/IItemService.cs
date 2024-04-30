using Chat.Domain.Entities;

namespace Chat.Application.Abstractions.Grpc;

public interface IItemService
{
    Task<Item?> GetByIdAsync(Guid id, CancellationToken token = default);
}