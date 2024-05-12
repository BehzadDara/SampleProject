using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.BaseModels;

namespace SampleProject.Infrastructure.Implementations;

public class BaseRepository<TEntity>(
    BaseDBContext dbContext,
    ICurrentUser currentUser
    ) : BaseReadOnlyRepository<TEntity>(dbContext),
    IBaseRepository<TEntity> where TEntity : Entity
{
    protected DbSet<TEntity> Set => dbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is TrackableEntity trackable)
        {
            trackable.Created(currentUser.UserName);
        }

        try
        {
            await Set.AddAsync(entity, cancellationToken);
        }
        catch
        {
        }
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
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
        }
        catch
        {
        }
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
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
        }
        catch
        {
        }
    }
}
