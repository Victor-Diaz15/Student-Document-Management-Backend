using System.Linq.Expressions;

namespace StudentDocumentManagement.Core.Domain.Abstractions;

public interface IGenericRepository<TEntity>
    where TEntity : class, IEntityBase
{
    Task<List<TEntity>> GetAllAsync();
    Task<List<TEntity>> GetAllWithIncludesAsync(List<string> props);
    Task<TEntity?> GetByIdWithIncludesAsync(Guid id, List<string> props);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task AddEntityAsync(TEntity TEntity);
    Task AddRangeEntityAsync(List<TEntity> entities);
    void UpdateEntityAsync(TEntity TEntity);
    void DeleteEntityAsync(TEntity TEntity);
    void SoftDeleteAsync(TEntity TEntity);
    IQueryable<TEntity> GetQueryable();
}
