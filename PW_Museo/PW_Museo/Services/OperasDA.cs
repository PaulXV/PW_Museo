using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

namespace PW_Museo.Services;

public class OperasDA : IOperasDA
{
    private readonly string _connectionString;

    public OperasDA(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionString:db"] ?? throw new InvalidOperationException("Connection string 'db' not found.");
    }

    public async Task<IEnumerable<Opera>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Opera>("SELECT * FROM Operas");
    }

    public async Task<Opera> GetByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Opera>("SELECT * FROM Operas WHERE Id = @Id", new { Id = id });
    }

    public async Task CreateAsync(Opera opera)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "INSERT INTO Operas (Id, Title, Description, creationYear, Techinic, Typology, ShowId, ImageId, AuthorId) VALUES (@Id, @Title, @Description, @creationYear, @Techinic, @Typology, @ShowId, @ImageId, @AuthorId)";
        await connection.ExecuteAsync(sql, opera);
    }

    public async Task UpdateAsync(Opera opera)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "UPDATE Operas SET Title = @Title, Description = @Description, creationYear = @creationYear, Techinic = @Techinic, Typology = @Typology, ShowId = @ShowId, ImageId = @ImageId, AuthorId = @AuthorId WHERE Id = @Id";
        await connection.ExecuteAsync(sql, opera);
    }

    public async Task DeleteAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "DELETE FROM Operas WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<IEnumerable<Opera>> GetAllDetailAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = """
            SELECT 
                o.Id, o.Title, o.Description, o.creationYear, o.Techinic, o.Typology,
                o.ShowId, s.Title AS ShowTitle,
                a.Name AS AuthorName, a.Surname AS AuthorSurname
            FROM Operas o
            LEFT JOIN Artists a ON o.AuthorId = a.Id
            LEFT JOIN Shows s ON o.ShowId = s.Id
            """;
        return await connection.QueryAsync<Opera>(sql);
    }
}