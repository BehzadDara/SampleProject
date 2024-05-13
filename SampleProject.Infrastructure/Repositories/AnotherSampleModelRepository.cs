using SampleProject.Domain.Interfaces;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;
using SampleProject.Infrastructure.Repositories.QueryTexts;

namespace SampleProject.Infrastructure.Repositories;

public class AnotherSampleModelRepository(
    BaseDBContext dbContext
    ) : BaseReadOnlyRepository<AnotherSampleModel>(dbContext), IAnotherSampleModelRepository
{
    public async Task<int> GetTotalCount(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.QueryGetAsync<int>(Queries.GetAnotherSampleModelTotalCount, cancellationToken);
        return result;
    }
}