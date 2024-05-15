using BuildingBlocks.Infrastructure.Implementations;
using SampleProject.Domain.Interfaces;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.QueryTexts;

namespace SampleProject.Infrastructure.Repositories;

public class AnotherSampleModelRepository(
    DBContext dbContext
    ) : ReadOnlyRepository<AnotherSampleModel>(dbContext), IAnotherSampleModelRepository
{
    public async Task<int> GetTotalCount(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.QueryGetAsync<int>(Queries.GetAnotherSampleModelTotalCount, cancellationToken);
        return result;
    }
}