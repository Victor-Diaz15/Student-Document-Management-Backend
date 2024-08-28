using Microsoft.EntityFrameworkCore;
using StudentDocumentManagement.Core.Domain.Abstractions;
using StudentDocumentManagement.Infrastructure.Persistence.Contexts;
using System.Linq.Expressions;

namespace StudentDocumentManagement.Infrastructure.Persistence.Repositories;

public class GenericBaseRepository<Entity> : IGenericRepository<Entity>
    where Entity : class, IEntityBase
{
    private readonly MainDbContext _dbContext;
    private readonly DbSet<Entity> _dbSet;
    public GenericBaseRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<Entity>();
    }

    /// <summary>
    /// Generic method to obtain a list of any entity in the system.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<List<Entity>> GetAllAsync() =>
        await _dbSet.ToListAsync();

    /// <summary>
    /// Generic method to obtain a list of any entity and its related entities
    /// </summary>
    /// <param name="props"></param>
    /// <returns></returns>
    public virtual async Task<List<Entity>> GetAllWithIncludesAsync(List<string> props)
    {
        var query = _dbSet.AsQueryable();

        foreach (string prop in props)
            query = query.Include(prop);

        return await query.ToListAsync();
    }

    /// <summary>
    /// Generic method to obtain a unique record but with includes.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="props"></param>
    /// <returns></returns>
    public virtual async Task<Entity?> GetByIdWithIncludesAsync(Guid id, List<string> props)
    {
        var query = _dbSet.AsQueryable();

        foreach (string prop in props)
            query = query.Include(prop);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Method that receives a lambda as a predicate, to search for said entity.
    /// And returns true if it exists and false if it does not exist.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate)
        => await _dbSet.AnyAsync(predicate);

    /// <summary>
    /// Generic method to obtain a specific entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<Entity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    /// <summary>
    /// Method to add a new entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task AddEntityAsync(Entity entity) =>
        await _dbSet.AddAsync(entity);

    /// <summary>
    /// Method to add a new list of entity.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task AddRangeEntityAsync(List<Entity> entities) =>
        await _dbSet.AddRangeAsync(entities);

    /// <summary>
    /// Method to edit an entity as needed.
    /// </summary>
    /// <param name="entity"></param>
    public virtual void UpdateEntityAsync(Entity entity) =>
        _dbSet.Update(entity);

    /// <summary>
    /// Method to delete any entity
    /// </summary>
    /// <param name="entity"></param>
    public virtual void DeleteEntityAsync(Entity entity) =>
        _dbSet.Remove(entity);

    /// <summary>
    /// Method to make a logical deletion of any record of any entity
    /// </summary>
    /// <param name="entity"></param>
    public virtual void SoftDeleteAsync(Entity entity)
    {
        var deleteEntity = entity.GetType();
        var prop = deleteEntity.GetProperty("Borrado");
        prop?.SetValue(entity, true);
    }

    /// <summary>
    /// Method to return an Iqueryable of the entity in question.
    /// </summary>
    /// <returns></returns>
    public virtual IQueryable<Entity> GetQueryable() => _dbSet.AsQueryable();
}
