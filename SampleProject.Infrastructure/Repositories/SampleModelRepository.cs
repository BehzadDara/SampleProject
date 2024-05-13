using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.Interfaces;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;
using SampleProject.Infrastructure.QueryTexts;

namespace SampleProject.Infrastructure.Repositories;

public class SampleModelRepository(
    BaseDBContext dbContext,
    ICurrentUser currentUser
    ) : BaseRepository<SampleModel>(dbContext, currentUser), ISampleModelRepository
{
    public async Task<int> GetTotalCount(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.QueryGetAsync<int>(Queries.GetSampleModelTotalCount, cancellationToken);
        return result;
    }
}