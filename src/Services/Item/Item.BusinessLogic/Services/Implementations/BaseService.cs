using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;

namespace Library.BLL.Services.Implementations;

public abstract class BaseService<TEntity, TEntityDto>(
    IRepository<TEntity> _entityRepository, IMapper _mapper)
    : IBaseService<TEntity, TEntityDto>
    where TEntity : BaseEntity
{
    public async virtual Task<TEntity> CreateAsync(TEntityDto entityDto, CancellationToken token)
    {
        var entity = _mapper.Map<TEntityDto, TEntity>(entityDto);
        var result = await _entityRepository.AddAsync(entity, token);

        return result;
    }

    public async virtual Task<TEntity> DeleteByIdAsync(Guid id, CancellationToken token)
    {
        var entity = await _entityRepository.GetByIdAsync(id, token);
        if (entity is null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} is not found");
        }

        await _entityRepository.DeleteAsync(entity, token);

        return entity;
    }

    public async virtual Task<IEnumerable<TEntity>> GetAsync(CancellationToken token)
    {
        var entities = await _entityRepository.GetAsync(token);

        return entities;
    }

    public async virtual Task<TEntity> GetByIdAsync(Guid id, CancellationToken token)
    {
        var entity = await _entityRepository.GetByIdAsync(id, token);
        if (entity is null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} is not found");
        }

        return entity;
    }

    public async virtual Task<TEntity> UpdateAsync(Guid id, TEntityDto entityDto, CancellationToken token)
    {
        var entity = await _entityRepository.GetByIdAsync(id, token);
        if (entity is null)
        {
            throw new NotFoundException($"{typeof(TEntity).Name} is not found");
        }

        _mapper.Map(entityDto, entity);

        var result = await _entityRepository.UpdateAsync(entity, token);

        return result;
    }
}
