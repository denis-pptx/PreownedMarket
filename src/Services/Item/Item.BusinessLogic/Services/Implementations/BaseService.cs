using AutoMapper;
using Item.BusinessLogic.Exceptions;
using Item.BusinessLogic.Exceptions.ErrorMessages;
using Item.BusinessLogic.Services.Interfaces;
using Item.DataAccess.Models;
using Item.DataAccess.Repositories.Interfaces;

namespace Library.BLL.Services.Implementations;

public abstract class BaseService<TEntity, TEntityDto> : IBaseService<TEntity, TEntityDto>
    where TEntity : BaseEntity
{
    protected readonly IRepository<TEntity> _entityRepository;
    protected readonly IMapper _mapper;

    public BaseService(IRepository<TEntity> entityRepository, IMapper mapper)
    {
        _entityRepository = entityRepository;
        _mapper = mapper;
    }

    public async virtual Task<TEntity> CreateAsync(TEntityDto entityDto, CancellationToken token)
    {
        var entity = _mapper.Map<TEntityDto, TEntity>(entityDto);
        var result = await _entityRepository.AddAsync(entity, token);

        return result;
    }

    public async virtual Task<TEntity> DeleteByIdAsync(Guid id, CancellationToken token)
    {
        var entity = await _entityRepository.GetByIdAsync(id, token);

        NotFoundException.ThrowIfNull(entity);
        
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

        NotFoundException.ThrowIfNull(entity);

        return entity;
    }

    public async virtual Task<TEntity> UpdateAsync(Guid id, TEntityDto entityDto, CancellationToken token)
    {
        var entity = await _entityRepository.GetByIdAsync(id, token);

        NotFoundException.ThrowIfNull(entity);

        _mapper.Map(entityDto, entity);

        var result = await _entityRepository.UpdateAsync(entity, token);

        return result;
    }
}