using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.BaseSpecificationConfig;
using SampleProject.Domain.Interfaces;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;

namespace SampleProject.Infrastructure.Repositories;

public class SampleModelRepository(
    BaseDBContext dbContext,
    ICurrentUser currentUser
    ) : BaseRepository<SampleModel>(dbContext, currentUser), ISampleModelRepository
{
    public async Task<(int TotalCount, IList<SampleModel> Data)> GetByFilter(BaseSpecification<SampleModel> specification, CancellationToken cancellationToken = default)
    {
        var query = Set.Specify(specification).Where(x => !x.IsDeleted);

        var totalCount = await query.CountAsync(cancellationToken);
        var data = await query.Skip(specification.Skip).Take(specification.Take).ToListAsync(cancellationToken);

        return (totalCount, data);
    }
}