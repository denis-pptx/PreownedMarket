using Item.BusinessLogic.Models.DTOs;

namespace Item.BusinessLogic.Services.Interfaces;

public interface IItemService 
    : IBaseService<DataAccess.Models.Item, ItemDto>
{
}