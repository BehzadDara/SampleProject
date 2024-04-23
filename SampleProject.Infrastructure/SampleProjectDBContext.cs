using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Models;
using SampleProject.Infrastructure.Implementations;

namespace SampleProject.Infrastructure;

public class SampleProjectDBContext : BaseDBContext
{
    protected SampleProjectDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<SampleModel> SampleModels { get; set; }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }*/
}
