using SampleProject.Domain.BaseModels;
using SampleProject.Domain.BaseSpecificationConfig;
using System.Data;

namespace SampleProject.Domain.BaseInterfaces;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<(int TotalCount, IList<TEntity> Data)> ListAsync(BaseSpecification<TEntity> specification, CancellationToken cancellationToken = default);
    public Task<TResult?> QueryGetAsync<TResult>(string query, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<TResult>> QueryListAsync<TResult>(string query, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);
}
