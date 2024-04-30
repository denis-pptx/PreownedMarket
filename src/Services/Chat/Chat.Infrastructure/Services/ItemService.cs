using Chat.Application.Abstractions.Grpc;
using Chat.Infrastructure.Protos;
using Grpc.Core;

namespace Chat.Infrastructure.Services;

using Item = Application.Models.Items.Item;

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

            return new Item(
                Guid.Parse(itemResponse.Id),
                itemResponse.Title,
                itemResponse.FirstImagePath,
                itemResponse.IsActive,
                Guid.Parse(itemResponse.UserId));
        }
        catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.NotFound)
        {
            return default;
        }
    }
}