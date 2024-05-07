using Microsoft.Extensions.Configuration;
using SampleProject.Domain.BaseInterfaces;
using SampleProject.Domain.Interfaces;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;
using SampleProject.Infrastructure.Repositories.QueryTexts;

namespace SampleProject.Infrastructure.Repositories;

public class SampleModelRepository(
    BaseDBContext dbContext,
    IConfiguration configuration,
    ICurrentUser currentUser
    ) : BaseRepository<SampleModel>(dbContext, configuration, currentUser), ISampleModelRepository
{
    public async Task<int> GetTotalCount(CancellationToken cancellationToken = default)
    {
        var result = await QueryGetAsync<int>(Queries.GetSampleModelTotalCount, cancellationToken: cancellationToken);
        return result;
    }
}