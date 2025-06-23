using appointmenting.Exceptions;
using appointmenting.RepositoryAbstractions.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace appointmenting.Repositories.Base;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class
{

    protected DbContext DbContext { get; }

    protected DbSet<TEntity> Entity => DbContext.Set<TEntity>();

    public RepositoryBase(DbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

        DbContext = dbContext;
    }

    #region Add

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await Entity.AddAsync(entity, cancellationToken);

            return entity;
        }
        catch
        {
            throw;
        }

    }

    #endregion

    #region Update

    public virtual async Task<TEntity> UpdateAsync(Guid id, TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingEntity = await GetByIdAsync(id, cancellationToken) ??
                throw new EntityNotFoundException($"{typeof(TEntity).ShortDisplayName()} with id not found. Id: {id}");

            ////Todo: Check for negative side effect
            //var ignoredProperties = Entity.Entry(existingEntity).GetIgnoredProperties();

            //if (ignoredProperties.Any())
            //    // Replace entity value with existing entity value
            //    foreach (var property in ignoredProperties)
            //        Entity.Entry(entity).Property(property.Metadata.Name).CurrentValue = Entity.Entry(existingEntity).Property(property.Metadata.Name).CurrentValue;

            Entity.Entry(existingEntity).CurrentValues.SetValues(entity);

            return existingEntity;
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Remove

    public virtual async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetByIdAsync(id, cancellationToken) ??
                throw new EntityNotFoundException($"{typeof(TEntity).ShortDisplayName()} with id not found. Id: {id}");

            Entity.Remove(entity);
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region GetById

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await Entity.FindAsync([id], cancellationToken);

        return result;
    }

    #endregion

    #region SingleOrDefault

    public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = Entity.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();

            var result = await query.Where(predicate).SingleOrDefaultAsync(cancellationToken);
            return result ?? default!;
        }
        catch
        {
            throw;
        }
    }

    //public virtual async Task<TEntity> SingleOrDefaultAsync(ICriteriaBuilder<TEntity> queryBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var query = Entity.CreateQuery(queryBuilder);
    //        var result = await query.SingleOrDefaultAsync(cancellationToken);

    //        return result ?? default!;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public virtual async Task<TResult> SingleOrDefaultAsync<TResult>(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var query = Entity.CreateQuery(criteriaBuilder);
    //        var result = await query
    //            .ProjectTo<TResult>(_mapper?.ConfigurationProvider)
    //            .SingleOrDefaultAsync(cancellationToken);

    //        return result ?? default!;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}


    #endregion

    #region FirstOrDefault

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = Entity.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();

            var result = await query.Where(predicate).FirstOrDefaultAsync(cancellationToken);

            return result ?? default!;
        }
        catch
        {
            throw;
        }
    }

    //public virtual async Task<TEntity> FirstOrDefaultAsync(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var query = Entity.CreateQuery(criteriaBuilder);
    //        var result = await query.FirstOrDefaultAsync(cancellationToken);

    //        return result ?? default!;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public virtual async Task<TResult> FirstOrDefaultAsync<TResult>(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var query = Entity.CreateQuery(criteriaBuilder);
    //        var result = await query
    //            .ProjectTo<TResult>(_mapper?.ConfigurationProvider)
    //            .FirstOrDefaultAsync(cancellationToken);

    //        return result ?? default!;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    #endregion

    #region List

    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = Entity.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();

            return await query.Where(predicate).ToListAsync(cancellationToken);
        }
        catch
        {
            throw;
        }
    }

    //public virtual async Task<List<TEntity>> GetListAsync(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var query = Entity.CreateQuery(criteriaBuilder);
    //        return await query.ToListAsync(cancellationToken);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public virtual async Task<List<TResult>> GetListAsync<TResult>(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var query = Entity.CreateQuery(criteriaBuilder);
    //        return await query
    //            .ProjectTo<TResult>(_mapper?.ConfigurationProvider)
    //            .ToListAsync(cancellationToken);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public virtual async Task<PagedList<TEntity>?> GetPagedListAsync(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        int? pageIndex = 0;
    //        int? pageSize = 0;

    //        pageIndex = criteriaBuilder.Query.Skip;
    //        pageSize = criteriaBuilder.Query.Take;

    //        var totalCount = await CountAsync(criteriaBuilder
    //            .RemoveSkip()
    //            .RemoveTake(),
    //            cancellationToken);

    //        var query = Entity.CreateQuery(criteriaBuilder);

    //        //if (pageIndex <= 0) pageIndex = 1;
    //        if (pageIndex < 0) pageIndex = PagedListOptions.DefaultPageIndex;
    //        if (pageSize < 1) pageSize = PagedListOptions.DefaultPageSize;

    //        var results = await query
    //            //.Skip((pageIndex!.Value - 1) * pageSize!.Value)
    //            .Skip(pageIndex!.Value * pageSize!.Value)
    //            .Take(pageSize.Value)
    //            .ToListAsync(cancellationToken);

    //        return new PagedList<TEntity>(results, pageIndex!.Value, pageSize!.Value, totalCount);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //public virtual async Task<PagedList<TResult>?> GetPagedListAsync<TResult>(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        int? pageIndex = 0;
    //        int? pageSize = 0;

    //        pageIndex = criteriaBuilder.Query.Skip;
    //        pageSize = criteriaBuilder.Query.Take;

    //        var totalCount = await CountAsync(criteriaBuilder
    //            .RemoveSkip()
    //            .RemoveTake(),
    //            cancellationToken);

    //        var query = Entity.CreateQuery(criteriaBuilder);

    //        //if (pageIndex <= 0) pageIndex = 1;
    //        if (pageIndex < 0) pageIndex = PagedListOptions.DefaultPageIndex;
    //        if (pageSize < 1) pageSize = PagedListOptions.DefaultPageSize;

    //        var results = await query
    //            //.Skip((pageIndex!.Value - 1) * pageSize!.Value)
    //            .Skip(pageIndex!.Value * pageSize!.Value)
    //            .Take(pageSize.Value)
    //            .ProjectTo<TResult>(_mapper?.ConfigurationProvider)
    //            .ToListAsync(cancellationToken);

    //        return new PagedList<TResult>(results, pageIndex!.Value, pageSize!.Value, totalCount);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    #endregion

    #region Count

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = Entity.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();

            return await query.Where(predicate).CountAsync(cancellationToken);
        }
        catch
        {
            throw;
        }
    }

    //public virtual async Task<int> CountAsync(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var query = Entity.CreateQuery(criteriaBuilder);

    //        return await query.CountAsync(cancellationToken);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    #endregion

    #region Any

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var query = Entity.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();

            return await query.Where(predicate).AnyAsync(cancellationToken);
        }
        catch
        {
            throw;
        }
    }

    //public virtual async Task<bool> AnyAsync(ICriteriaBuilder<TEntity> criteriaBuilder,
    //    CancellationToken cancellationToken = default)
    //{
    //    try
    //    {
    //        var query = Entity.CreateQuery(criteriaBuilder);

    //        return await query.AnyAsync(cancellationToken);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    #endregion
}
