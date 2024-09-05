using System.Linq.Expressions;

namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IGenericBaseRepository<TEntity>
    where TEntity : class, IEntityBase
{
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAllWithIncludesAsync(List<string> props);
    Task<TEntity?> GetByIdWithIncludesAsync(Guid id, List<string> props);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task AddEntityAsync(TEntity TEntity);
    Task AddRangeEntityAsync(List<TEntity> entities);
    void UpdateEntity(TEntity TEntity);
    void DeleteEntity(TEntity TEntity);
    void SoftDelete(TEntity TEntity);
    IQueryable<TEntity> GetQueryable();
}
