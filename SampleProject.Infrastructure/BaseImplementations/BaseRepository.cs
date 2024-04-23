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
            trackable.CreatedAt = DateTime.Now;
            trackable.CreatedBy = "Test";
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
            trackable.UpdatedAt = DateTime.Now;
            trackable.UpdatedBy = "Test";
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
                trackable.IsDeleted = true;
                trackable.DeletedAt = DateTime.Now;
                trackable.DeletedBy = "Test";

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
        var entity = await Set.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is TrackableEntity trackable && trackable.IsDeleted)
        {
            return null;
        }
        
        return entity;
    }

    /*public Task<TEntity> GetAsync(Specification<T> spec, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IList<TEntity>> ListAsync(Specification<T> spec, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }*/

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await Set.ToListAsync(cancellationToken);

        return list;
    }
}
