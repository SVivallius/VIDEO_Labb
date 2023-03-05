using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using VIDEO.Membership.data.Entities;

namespace VIDEO.Membership.data.Service;
public interface IDbService
{
    public Task<TEntity> CreateAsync<TEntity, TDto>(TDto dto)
        where TEntity : class, IEntity
        where TDto : class;

    public Task<List<TDto>> GetAllAsync<TEntity, TDto>()
        where TEntity : class, IEntity
        where TDto : class;

    public Task<List<TDto>> GetFilteredAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        where TEntity: class, IEntity
        where TDto: class;

    public Task<TDto> GetByIdAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
        where TDto : class;

    public void Update<TEntity, TDto>(int id, TDto dto)
        where TEntity : class, IEntity
        where TDto : class;

    public Task<bool> DeleteAsync<TEntity, TDto>(int id)
        where TEntity : class, IEntity
        where TDto : class;

    public Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity;

    public Task<bool> SaveChangesAsync();

    public void Include<TEntity>()
        where TEntity : class, IEntity;
}
