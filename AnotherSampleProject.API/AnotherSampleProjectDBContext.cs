using AnotherSampleProject.API.Model;
using Microsoft.EntityFrameworkCore;

namespace AnotherSampleProject.API;

public class AnotherSampleProjectDBContext(DbContextOptions<AnotherSampleProjectDBContext> options) : DbContext(options)
{
    public DbSet<TestModel> TestModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TestModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).UseSerialColumn();
        });
    }
}