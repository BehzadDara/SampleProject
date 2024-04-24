using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.BaseModels;
using System.Linq.Expressions;
using System.Reflection;

namespace SampleProject.Infrastructure.Implementations;

public abstract class BaseDBContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        SetEnumToStringValueConverter(modelBuilder);
    }

    private static void SetEnumToStringValueConverter(ModelBuilder modelBuilder)
    {
        var enumProperties = modelBuilder
            .Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType.IsEnum);

        foreach (var enumProperty in enumProperties)
        {
            var type = typeof(EnumToStringConverter<>).MakeGenericType(enumProperty.ClrType);

            var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;
            enumProperty.SetValueConverter(converter);
        }
    }
}
