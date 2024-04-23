using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseModels;
using System.Linq.Expressions;
using System.Reflection;

namespace SampleProject.Infrastructure.Implementations;

public abstract class BaseDBContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        StoreAndRetrieveEnumsAsString(modelBuilder);
    }

    private static void StoreAndRetrieveEnumsAsString(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var properties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType.IsEnum);

            foreach (var property in properties)
            {
                /*modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(
                        v => v.ToString(),
                        v => Enum.Parse(property.PropertyType, v));*/
            }
        }
    }
}
