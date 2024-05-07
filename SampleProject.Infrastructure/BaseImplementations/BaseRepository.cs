using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.BaseModels;
using SampleProject.Domain.BaseSpecificationConfig;
using System.Data;

namespace SampleProject.Infrastructure.Implementations;

public class BaseRepository<TEntity>(
    BaseDBContext dbContext,
    SqlConnection connection,
    ICurrentUser currentUser
    ) : IBaseRepository<TEntity>, IDisposable where TEntity : Entity
{
    protected DbSet<TEntity> Set => dbContext.Set<TEntity>();
    protected IQueryable<TEntity> SetAsNoTracking
    {
        get
        {
            var query = Set.AsNoTracking();

            if (typeof(TEntity).IsSubclassOf(typeof(TrackableEntity)))
            {
                query = query.Where(e => !(e as TrackableEntity)!.IsDeleted);
            }

            return query;
        }
    }


    public async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is TrackableEntity trackable)
        {
            trackable.Created(currentUser.UserName);
        }

        try
        {
            await Set.AddAsync(entity, cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is TrackableEntity trackable)
        {
            trackable.Updated(currentUser.UserName);
        }

        try
        {
            await Task.Run(() =>
            {
                dbContext.Update(entity);
            }, cancellationToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            if (entity is TrackableEntity trackable)
            {
                trackable.Deleted(currentUser.UserName);

                await Task.Run(() =>
                {
                    dbContext.Update(entity);
                }, cancellationToken);
            }
            else
            {
                await Task.Run(() =>
                {
                    dbContext.Remove(entity);
                }, cancellationToken);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await SetAsNoTracking.Specify(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<(int TotalCount, IList<TEntity> Data)> ListAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        var query = SetAsNoTracking.Specify(specification);

        var totalCount = await query.CountAsync(cancellationToken);
        var data = await query.Skip(specification.Skip).Take(specification.Take).ToListAsync(cancellationToken);

        return (totalCount, data);
    }

    public async Task<TResult?> QueryGetAsync<TResult>(string query, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        if (connection.State == ConnectionState.Closed)
            await connection.OpenAsync(cancellationToken);
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await connection.QueryFirstOrDefaultAsync<TResult>(query, param, transaction);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

    public async Task<IReadOnlyList<TResult>> QueryListAsync<TResult>(string query, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    {
        if (connection.State == ConnectionState.Closed)
            await connection.OpenAsync(cancellationToken);

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await connection.QueryAsync<TResult>(query, param, transaction);
            return result.AsList();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

    public void Dispose()
    {
        connection.Dispose();
        GC.SuppressFinalize(this);
    }
}
