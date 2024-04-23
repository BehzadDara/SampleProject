using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.BaseModels;
using System.Reflection;

namespace SampleProject.Infrastructure.Implementations;

public class BaseDBContext : DbContext
{
    protected BaseDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //DefineKeyForEntities(modelBuilder);

        StoreAndRetrieveEnumsAsString(modelBuilder);
    }

    private static void DefineKeyForEntities(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.ClrType.IsGenericType &&
                        e.ClrType.GetGenericTypeDefinition() == typeof(Entity));

        foreach (var entityType in entityTypes)
        {
            var entityTypeArgument = entityType.ClrType.GetGenericArguments()[0];
            var idProperty = entityTypeArgument.GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            modelBuilder.Entity(entityType.ClrType).HasKey("Id");
        }
    }

    private static void StoreAndRetrieveEnumsAsString(ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var properties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType.IsEnum);

            /*foreach (var property in properties)
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(
                        v => v.ToString(),
                        v => Enum.Parse(property.PropertyType, v));
            }*/
        }
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
