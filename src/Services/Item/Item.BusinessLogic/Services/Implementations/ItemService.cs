using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Models.DTOs;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Data.Initializers.Values;
using Item.DataAccess.Enums;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;
using Item.DataAccess.Specifications.Implementations;
using Library.BLL.Services.Implementations;

namespace Item.BusinessLogic.Services.Implementations;

public class ItemService(
    IRepository<DataAccess.Models.Item> entityRepository, 
    IRepository<Status> _statusRepository,
    ICurrentUserService currentUserService,
    IMapper mapper) 
    : BaseService<DataAccess.Models.Item, ItemDto>(entityRepository, mapper), IItemService
{
    public async override Task<DataAccess.Models.Item> CreateAsync(ItemDto itemDto, CancellationToken token)
    {
        var item = _mapper.Map<ItemDto, DataAccess.Models.Item>(itemDto);

        item.CreatedAt = DateTime.UtcNow;

        var status = await _statusRepository.SingleOrDefaultAsync(
            x => x.NormalizedName == StatusValues.UnderReview.NormalizedName, token);
        item.StatusId = status!.Id;

        var currentUserId = currentUserService.UserId ?? throw new UnauthorizedException();
        item.UserId = currentUserId;

        var result = await _entityRepository.AddAsync(item, token);

        return result;
    }

    public async override Task<DataAccess.Models.Item> UpdateAsync(Guid id, ItemDto itemDto, CancellationToken token)
    {
        var item = await _entityRepository.GetByIdAsync(id, token) ?? 
            throw new NotFoundException(GenericErrorMessages<DataAccess.Models.Item>.NotFound);

        var currentUserId = currentUserService.UserId ?? 
            throw new UnauthorizedException();

        var currentRole = currentUserService.Role ?? 
            throw new UnauthorizedException();

        if (currentUserId == item.UserId ||
            currentRole == nameof(Role.Administrator) ||
            currentRole == nameof(Role.Moderator))
        {
            _mapper.Map(itemDto, item);

            var status = await _statusRepository.SingleOrDefaultAsync(
                x => x.NormalizedName == StatusValues.UnderReview.NormalizedName, token);

            item.StatusId = status!.Id;

            var result = await _entityRepository.UpdateAsync(item, token);

            return result;
        }
        else
        {
            throw new ForbiddenException();
        }
    }

    public async override Task<DataAccess.Models.Item> DeleteByIdAsync(Guid id, CancellationToken token)
    {
        var item = await _entityRepository.GetByIdAsync(id, token) ?? 
            throw new NotFoundException(GenericErrorMessages<DataAccess.Models.Item>.NotFound);

        var currentUserId = currentUserService.UserId ??
            throw new UnauthorizedException();

        var currentRole = currentUserService.Role ??
            throw new UnauthorizedException();

        if (currentUserId == item.UserId ||
            currentRole == nameof(Role.Administrator) ||
            currentRole == nameof(Role.Moderator))
        {
            await _entityRepository.DeleteAsync(item, token);

            return item;
        }
        else
        {
            throw new ForbiddenException();
        }
    }

    public async Task<DataAccess.Models.Item> ChangeStatus(Guid id, UpdateStatusDto updateStatusDto, CancellationToken token = default)
    {
        var specification = new ItemWithStatusSpecification(id);
        var item = await _entityRepository.FirstOrDefaultAsync(specification, token) ??
            throw new NotFoundException(GenericErrorMessages<DataAccess.Models.Item>.NotFound);

        Status currentStatus = item.Status!;

        Status newStatus = await _statusRepository.FirstOrDefaultAsync(x => x.NormalizedName == updateStatusDto.NormalizedName, token) ??
            throw new NotFoundException(GenericErrorMessages<Status>.NotFound);

        var currentUserId = currentUserService.UserId ?? 
            throw new UnauthorizedException();

        var currentRole = currentUserService.Role ?? 
            throw new UnauthorizedException();

        if (currentUserId == item.UserId)
        {
            if ((currentStatus.Equals(StatusValues.Active) && newStatus.Equals(StatusValues.Inactive)) ||
                (currentStatus.Equals(StatusValues.Inactive) && newStatus.Equals(StatusValues.Active)))
            {
                item.Status = newStatus;
            }
            else
            {
                throw new ConflictException(ItemErrorMessages.StatusFailure);
            }
        }
        else if (currentRole == nameof(Role.Administrator) || 
            currentRole == nameof(Role.Moderator))
        {
            item.Status = newStatus;
        }
        else
        {
            throw new ForbiddenException();
        }

        return await _entityRepository.UpdateAsync(item, token);
    }
}