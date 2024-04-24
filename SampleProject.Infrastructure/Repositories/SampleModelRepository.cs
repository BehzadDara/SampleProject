using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseSpecificationConfig;
using SampleProject.Domain.Interfaces;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;

namespace SampleProject.Infrastructure.Repositories;

public class SampleModelRepository(BaseDBContext dbContext) : BaseRepository<SampleModel>(dbContext), ISampleModelRepository
{
    public async Task<(int TotalCount, IList<SampleModel> Data)> GetByFilter(BaseSpecification<SampleModel> specification, CancellationToken cancellationToken = default)
    {
        var query = Set.Specify(specification).Where(x => !x.IsDeleted);

        var totalCount = await query.CountAsync(x => !x.IsDeleted, cancellationToken);
        var data = await query.Skip(specification.Skip).Take(specification.Take).ToListAsync(cancellationToken);

        return (totalCount, data);
    }
}