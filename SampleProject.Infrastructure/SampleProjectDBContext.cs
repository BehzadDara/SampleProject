using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;

namespace SampleProject.Infrastructure;

public class SampleProjectDBContext(DbContextOptions<SampleProjectDBContext> options) : BaseDBContext(options)
{
    public DbSet<SampleModel> SampleModels { get; set; }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }*/
}
