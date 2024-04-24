using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.BaseModels;

namespace SampleProject.Infrastructure.Implementations;

public class BaseRepository<TEntity>(BaseDBContext _dbContext) : IBaseRepository<TEntity> where TEntity : Entity
{
    protected DbSet<TEntity> Set => _dbContext.Set<TEntity>();

    public async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity is TrackableEntity trackable)
        {
            trackable.Created("User Create");
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
            trackable.Updated("User Update");
        }

        try
        {
            await Task.Run(() =>
            {
                _dbContext.Update(entity);
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
                trackable.Deleted("User Delete");

                await Task.Run(() =>
                {
                    _dbContext.Update(entity);
                }, cancellationToken);
            }
            else
            {
                await Task.Run(() =>
                {
                    _dbContext.Remove(entity);
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
        var entity = await Set.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is TrackableEntity trackable && trackable.IsDeleted)
        {
            return null;
        }
        
        return entity;
    }

    /*public Task<IList<TEntity>> ListAsync(Specification<T> spec, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }*/

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = Set.AsNoTracking();

        if (typeof(TEntity).IsSubclassOf(typeof(TrackableEntity)))
        {
            query = query.Where(e => !(e as TrackableEntity)!.IsDeleted);
        }

        var list = await query.ToListAsync(cancellationToken);

        return list;
    }
}
