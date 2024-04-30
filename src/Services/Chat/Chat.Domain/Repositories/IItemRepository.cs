namespace Chat.Domain.Repositories;

public interface IItemRepository
{
    Task DeleteByUserIdAsync(Guid userId, CancellationToken token = default);   
}