using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

namespace PW_Museo.Services;

public class ImagesDA : IImagesDA
{
    private readonly string _connectionString;

    public ImagesDA(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionString:db"]!;
    }

    public async Task<Guid> CreateAsync(string src)
    {
        using var connection = new SqlConnection(_connectionString);
        var id = Guid.NewGuid();
        await connection.ExecuteAsync("INSERT INTO Images (Id, Src) VALUES (@Id, @Src)", new { Id = id, Src = src });
        return id;
    }

    public async Task<Image?> GetByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Image>("SELECT * FROM Images WHERE Id = @Id", new { Id = id });
    }

    public async Task DeleteAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync("DELETE FROM Images WHERE Id = @Id", new { Id = id });
    }
}