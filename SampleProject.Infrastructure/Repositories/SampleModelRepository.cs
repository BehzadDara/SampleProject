using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;

namespace SampleProject.Infrastructure.Repositories;

public class SampleModelRepository(BaseDBContext _dbContext) : BaseRepository<SampleModel>(_dbContext)
{
}