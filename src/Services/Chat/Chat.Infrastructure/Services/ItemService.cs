using Chat.Application.Abstractions.Grpc;
using Chat.Infrastructure.Protos;
using Grpc.Core;

namespace Chat.Infrastructure.Services;

using Item = Domain.Entities.Item;

public class ItemService(Protos.Item.ItemClient _itemClient) 
    : IItemService
{
    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var request = new GetItemRequest()
        {
            Id = id.ToString()
        };

        try
        {
            var itemResponse = await _itemClient.GetItemAsync(request, cancellationToken: token);

            return new Item
            {
                Id = Guid.Parse(itemResponse.Id),
                Title = itemResponse.Title,
                FirstImagePath = itemResponse.FirstImagePath,
                UserId = Guid.Parse(itemResponse.UserId),
            };
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.NotFound)
        {
            return default;
        }
    }
}