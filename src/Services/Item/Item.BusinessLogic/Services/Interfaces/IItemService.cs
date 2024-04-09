using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Item.BusinessLogic.Services.Interfaces;

using Item = DataAccess.Models.Item;

public interface IItemService 
{
    Task<Item> GetByIdAsync(Guid id, CancellationToken token);
    Task<PagedList<Item>> GetAsync(
        string? searchTerm, 
        Guid? cityId, 
        string? categoryNormalizedName, 
        string? statusNormalizedName, 
        Guid? userId, 
        string? sortColumn, 
        string? sortOrder, 
        int page, 
        int pageSize, 
        CancellationToken token = default);
    Task<Item> ChangeStatus(Guid id, UpdateStatusDto updateStatusDto, CancellationToken token = default);
    Task<Item> CreateAsync(ItemDto itemDto, CancellationToken token);
    Task<Item> UpdateAsync(Guid id, ItemDto itemDto, CancellationToken token);
    Task<Item> DeleteByIdAsync(Guid id, CancellationToken token);
}