using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Models.Common;
using Microsoft.AspNetCore.Http;
using Item.BusinessLogic.Models.DTOs.Filter;

namespace Item.BusinessLogic.Services.Interfaces;

using Item = DataAccess.Models.Item;

public interface IItemService 
{
    Task<Item> GetByIdAsync(Guid id, CancellationToken token);
    Task<PagedList<Item>> GetAsync(ItemFilterQuery filterQuery, CancellationToken token = default);
    Task<Item> ChangeStatus(Guid id, UpdateStatusDto updateStatusDto, CancellationToken token = default);
    Task<Item> CreateAsync(ItemDto itemDto, CancellationToken token);
    Task<Item> UpdateAsync(Guid id, ItemDto itemDto, CancellationToken token);
    Task<Item> DeleteByIdAsync(Guid id, CancellationToken token);
}