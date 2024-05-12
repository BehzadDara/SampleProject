using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;

namespace SampleProject.Infrastructure;

public class AnotherSampleProjectDBContext(
    DbContextOptions<AnotherSampleProjectDBContext> options
    ) : BaseDBContext(options)
{
    public DbSet<AnotherSampleModel> AnotherSampleModels { get; set; }
}
