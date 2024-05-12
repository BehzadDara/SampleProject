using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.BaseModels;

namespace SampleProject.Infrastructure.Implementations;

public class BaseRepository<TEntity>(
    BaseDBContext dbContext,
    SqlConnection connection,
    ICurrentUser currentUser
    ) : BaseReadOnlyRepository<TEntity>(dbContext, connection),
    IBaseRepository<TEntity> where TEntity : Entity
{
    protected DbSet<TEntity> Set => dbContext.Set<TEntity>();

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
}
