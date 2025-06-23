using System.Linq.Expressions;

namespace appointmenting.RepositoryAbstractions.Base;

public interface IRepositoryBase<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(Guid id, TEntity entity, CancellationToken cancellationToken = default);
    Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true, CancellationToken cancellationToken = default);

    //Task<TEntity> SingleOrDefaultAsync(ICriteriaBuilder<TEntity> queryBuilder, CancellationToken cancellationToken = default);

    //Task<TResult> SingleOrDefaultAsync<TResult>(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);
    //Task<TEntity> FirstOrDefaultAsync(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);

    //Task<TResult> FirstOrDefaultAsync<TResult>(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);

    //Task<List<TEntity>> GetListAsync(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);

    //Task<List<TResult>> GetListAsync<TResult>(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);

    //Task<PagedList<TEntity>?> GetPagedListAsync(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);

    //Task<PagedList<TResult>?> GetPagedListAsync<TResult>(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);


    //Task<int> CountAsync(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);


    //Task<bool> AnyAsync(ICriteriaBuilder<TEntity> criteriaBuilder, CancellationToken cancellationToken = default);
}
