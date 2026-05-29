using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

namespace PW_Museo.Services;

public class VisitsDA : IVisitsDA
{
    private readonly string _connectionString;

    public VisitsDA(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionString:db"] ?? throw new InvalidOperationException("Connection string 'db' not found.");
    }

    public async Task<IEnumerable<Visit>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Visit>("SELECT * FROM Visits");
    }

    public async Task<Visit?> GetByIdAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Visit>("SELECT * FROM Visits WHERE Id = @Id", new { Id = id });
    }

    public async Task CreateAsync(Visit visit)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "INSERT INTO Visits (Id, Title, Description, DateTime, Duration, MaxParticipants, ShowId, GuideId, AuthorId) VALUES (@Id, @Title, @Description, @DateTime, @Duration, @MaxParticipants, @ShowId, @GuideId, @AuthorId)";
        await connection.ExecuteAsync(sql, visit);
    }

    public async Task UpdateAsync(Visit visit)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "UPDATE Visits SET Title = @Title, Description = @Description, DateTime = @DateTime, Duration = @Duration, MaxParticipants = @MaxParticipants, ShowId = @ShowId, GuideId = @GuideId, AuthorId = @AuthorId WHERE Id = @Id";
        await connection.ExecuteAsync(sql, visit);
    }

    public async Task DeleteAsync(Guid id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "DELETE FROM Visits WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
