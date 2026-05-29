using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

namespace PW_Museo.Services;

public class ArtistsDA : IArtistsDA
{
    private readonly string _connectionString;

    public ArtistsDA(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionString:db"] ?? throw new InvalidOperationException("Connection string 'db' not found.");
    }

    public async Task<IEnumerable<Artist>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Artist>("SELECT * FROM Artists");
    }

    public async Task<Artist?> GetByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Artist>("SELECT * FROM Artists WHERE Id = @Id", new { Id = id });
    }

    public async Task CreateAsync(Artist artist)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "INSERT INTO Artists (Id, Name, Surname, EmailAddress, IsAdmin, OperaId) VALUES (@Id, @Name, @Surname, @EmailAddress, @IsAdmin, @OperaId)";
        await connection.ExecuteAsync(sql, artist);
    }

    public async Task UpdateAsync(Artist artist)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "UPDATE Artists SET Name = @Name, Surname = @Surname, EmailAddress = @EmailAddress, IsAdmin = @IsAdmin, OperaId = @OperaId WHERE Id = @Id";
        await connection.ExecuteAsync(sql, artist);
    }

    public async Task DeleteAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync("DELETE FROM Artists WHERE Id = @Id", new { Id = id });
    }
}