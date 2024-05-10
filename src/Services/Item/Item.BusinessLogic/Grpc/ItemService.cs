using Grpc.Core;
using Item.BusinessLogic.Protos;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Data.Initializers.Values;
using Item.DataAccess.ErrorMessages;
using Shared.Errors.Messages;

namespace Item.BusinessLogic.Grpc;

using Item = DataAccess.Models.Entities.Item;

public class ItemService(IItemRepository _itemRepository)
    : Protos.Item.ItemBase
{
    public override async Task<GetItemResponse> GetItem(GetItemRequest request, ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            throw new RpcException(new Status(
                StatusCode.InvalidArgument,
                CommonErrorMessages.InvalidGuid.Description));
        }

        var item = await _itemRepository.GetByIdAsync(id, context.CancellationToken);

        if (item is null)
        {
            throw new RpcException(new Status(
                StatusCode.NotFound,
                GenericErrorMessages<Item>.NotFound.Description));
        }

        return new GetItemResponse()
        {
            Id = item.Id.ToString(),
            Title = item.Title,
            FirstImagePath = item.Images.FirstOrDefault()?.FilePath,
            IsActive = item.Status!.Equals(StatusValues.Active),
            UserId = item.UserId.ToString()
        };
    }
}