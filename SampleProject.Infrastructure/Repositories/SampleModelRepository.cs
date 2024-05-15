using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Infrastructure.Implementations;
using SampleProject.Domain.Interfaces;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.QueryTexts;

namespace SampleProject.Infrastructure.Repositories;

public class SampleModelRepository(
    DBContext dbContext,
    ICurrentUser currentUser
    ) : Repository<SampleModel>(dbContext, currentUser), ISampleModelRepository
{
    public async Task<int> GetTotalCount(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.QueryGetAsync<int>(Queries.GetSampleModelTotalCount, cancellationToken);
        return result;
    }
}