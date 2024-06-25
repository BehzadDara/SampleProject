using BuildingBlocks.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Models;

namespace SampleProject.Infrastructure;

public class SampleProjectDBContext(
    DbContextOptions<SampleProjectDBContext> options
    ) : DBContext(options)
{
    public DbSet<SampleModel> SampleModels { get; set; }
    public DbSet<AnotherSampleModel> AnotherSampleModels { get; set; }
}
