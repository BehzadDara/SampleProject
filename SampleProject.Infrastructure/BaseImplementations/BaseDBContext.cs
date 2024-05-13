using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data;

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

    public async Task<TResult?> QueryGetAsync<TResult>(string query, CancellationToken cancellationToken)
    {
        var queryable = await QueryAsync<TResult>(query, cancellationToken);
        return await queryable.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> QueryListAsync<TResult>(string query, CancellationToken cancellationToken)
    {
        var queryable = await QueryAsync<TResult>(query, cancellationToken);
        return await queryable.ToListAsync(cancellationToken);
    }

    private async Task<IQueryable<TResult>> QueryAsync<TResult>(string query, CancellationToken cancellationToken)
    {
        if (Database.GetDbConnection().State == ConnectionState.Closed)
            await Database.OpenConnectionAsync(cancellationToken);

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Database.SqlQueryRaw<TResult>(query);
        }
        finally
        {
            if (Database.GetDbConnection().State == ConnectionState.Open)
                await Database.CloseConnectionAsync();
        }
    }
}
