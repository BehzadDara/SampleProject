using BuildingBlocks.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Models;

namespace SampleProject.Infrastructure;

public class AnotherSampleProjectDBContext(
    DbContextOptions<AnotherSampleProjectDBContext> options
    ) : DBContext(options)
{
    public DbSet<AnotherSampleModel> AnotherSampleModels { get; set; }
}
