using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Dapper;
using GenericDataAccess.Interfaces;

namespace GenericDataAccess.Repositories;

/// <summary>
/// Generic repository implementation using Dapper and SQL Server.
/// </summary>
/// <typeparam name="T">The entity type that implements IEntity.</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    private readonly string _connectionString;
    private readonly string _tableName;

    public GenericRepository(string connectionString, string? tableName = null)
    {
        _connectionString = connectionString;
        _tableName = tableName ?? typeof(T).Name;
    }

    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var sql = $"SELECT * FROM [{_tableName}] WHERE Id = @Id";
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM [{_tableName}] WHERE Id = @Id";
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM [{_tableName}]";
        using var connection = CreateConnection();
        return await connection.QueryAsync<T>(sql);
    }

    public async Task<T> PostAsync(T entity)
    {
        var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToArray();
        var columns = string.Join(", ", properties.Select(p => $"[{p.Name}]"));
        var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
        var sql = $"INSERT INTO [{_tableName}] ({columns}) OUTPUT INSERTED.Id VALUES ({values})";
        using var connection = CreateConnection();
        var id = await connection.ExecuteScalarAsync<Guid>(sql, entity);
        entity.Id = id;
        return entity;
    }

    public async Task<IEnumerable<T>> FilterAsync(string whereClause, object parameters)
    {
        var sql = $"SELECT * FROM [{_tableName}] WHERE {whereClause}";
        using var connection = CreateConnection();
        return await connection.QueryAsync<T>(sql, parameters);
    }

    public async Task<IEnumerable<T>> CursorPaginationAsync(int limit, string? lastCursorValue = null, string cursorField = "Id")
    {
        var sql = $"SELECT TOP (@Limit) * FROM [{_tableName}] ";
        if (!string.IsNullOrEmpty(lastCursorValue))
        {
            sql += $"WHERE {cursorField} > @LastCursorValue ";
        }
        sql += $"ORDER BY {cursorField}";
        using var connection = CreateConnection();
        return await connection.QueryAsync<T>(sql, new { Limit = limit, LastCursorValue = lastCursorValue });
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToArray();
        var setClause = string.Join(", ", properties.Select(p => $"[{p.Name}] = @{p.Name}"));
        var sql = $"UPDATE [{_tableName}] SET {setClause} WHERE Id = @Id";
        using var connection = CreateConnection();
        await connection.ExecuteAsync(sql, entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = $"DELETE FROM [{_tableName}] WHERE Id = @Id";
        using var connection = CreateConnection();
        var affected = await connection.ExecuteAsync(sql, new { Id = id });
        return affected > 0;
    }

    public async Task<int> CountAsync()
    {
        var sql = $"SELECT COUNT(*) FROM [{_tableName}]";
        using var connection = CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var sql = $"SELECT 1 FROM [{_tableName}] WHERE Id = @Id";
        using var connection = CreateConnection();
        var result = await connection.ExecuteScalarAsync<int?>(sql, new { Id = id });
        return result.HasValue;
    }
}