namespace Item.BusinessLogic.Services.Interfaces;

using System;
using Item = DataAccess.Models.Entities.Item;

public interface ILikeService
{
    Task<IEnumerable<Item>> GetLikedItemsAsync(CancellationToken token = default);
    Task LikeItemAsync(Guid itemId, CancellationToken token = default);
    Task UnlikeItemAsync(Guid itemId, CancellationToken token = default);
}