using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace BuildingBlocks.Infrastructure.Implementations;

public abstract class DBContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public async Task<TResult?> QueryGetAsync<TResult>(string query, CancellationToken cancellationToken)
    {
        var result = await QueryListAsync<TResult>(query, cancellationToken);

        return result.Count > 0 ? result[0] : default;
    }

    public async Task<IReadOnlyList<TResult>> QueryListAsync<TResult>(string query, CancellationToken cancellationToken)
    {
        if (Database.GetDbConnection().State == ConnectionState.Closed)
            await Database.OpenConnectionAsync(cancellationToken);

        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Database.SqlQueryRaw<TResult>(query).ToListAsync(cancellationToken);
        }
        finally
        {
            if (Database.GetDbConnection().State == ConnectionState.Open)
                await Database.CloseConnectionAsync();
        }
    }

    public async Task<TResult?> ExecuteStoredProcedureGetAsync<TResult>(string storedProcedureName, CancellationToken cancellationToken, params SqlParameter[] parameters)
    {
        var result = await ExecuteStoredProcedureListAsync<TResult>(storedProcedureName, cancellationToken, parameters);

        return result.Count > 0 ? result[0] : default;
    }

    public async Task<IReadOnlyList<TResult>> ExecuteStoredProcedureListAsync<TResult>(string storedProcedureName, CancellationToken cancellationToken, params SqlParameter[] parameters)
    {
        var results = new List<TResult>();

        var connection = Database.GetDbConnection();

        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = storedProcedureName;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(parameters);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync())
        {
            if (IsValueType(typeof(TResult)))
            {
                results.Add((TResult)reader[0]);
            }
            else
            {
                results.Add(MapData<TResult>(reader));
            }
        }

        await connection.CloseAsync();

        return results;
    }
    

    public async Task ExecuteStoredProcedureAsync(string storedProcedureName, CancellationToken cancellationToken, params SqlParameter[] parameters)
    {
        var connection = Database.GetDbConnection();

        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = storedProcedureName;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(parameters);

        await command.ExecuteReaderAsync(cancellationToken);

        await connection.CloseAsync();

    }

    private static bool IsValueType(Type type)
    {
        return type.IsValueType || type == typeof(string) || type == typeof(byte[]);
    }

    private static TResult MapData<TResult>(DbDataReader record)
    {
        var obj = Activator.CreateInstance<TResult>();

        foreach (var property in typeof(TResult).GetProperties())
        {
            try
            {
                if (!record.IsDBNull(record.GetOrdinal(property.Name)))
                {
                    property.SetValue(obj, record[property.Name]);
                }
            }
            catch 
            { 
            }
        }

        return obj;
    }
}
