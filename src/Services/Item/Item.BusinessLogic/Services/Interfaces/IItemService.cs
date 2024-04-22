using Item.BusinessLogic.Models.DTOs;
using Item.DataAccess.Models;
using Item.DataAccess.Models.Filter;

namespace Item.BusinessLogic.Services.Interfaces;

using Item = DataAccess.Models.Entities.Item;

public interface IItemService 
{
    Task<Item> GetByIdAsync(Guid id, CancellationToken token);
    Task<PagedList<Item>> GetAsync(ItemFilterRequest filterRequest, CancellationToken token = default);
    Task<Item> ChangeStatus(Guid id, UpdateStatusDto updateStatusDto, CancellationToken token = default);
    Task<Item> CreateAsync(ItemDto itemDto, CancellationToken token);
    Task<Item> UpdateAsync(Guid id, ItemDto itemDto, CancellationToken token);
    Task<Item> DeleteByIdAsync(Guid id, CancellationToken token);
}