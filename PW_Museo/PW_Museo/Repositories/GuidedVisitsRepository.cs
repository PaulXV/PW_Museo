using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

namespace PW_Museo.Repositories
{
    public class GuidedVisitsRepository
    {
        private readonly string _connectionString;

        public GuidedVisitsRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:db"] ?? throw new InvalidOperationException("Connection string 'db' not found.");
        }

        public async Task<IEnumerable<Guided_Visit>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Guided_Visit>("SELECT * FROM Guided_Visits");
        }

        public async Task<Guided_Visit?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Guided_Visit>("SELECT * FROM Guided_Visits WHERE Id = @Id", new { Id = id });
        }

        public async Task CreateAsync(Guided_Visit guidedVisit)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Guided_Visits (Id, Date, Status, PersonId, VisitId) VALUES (@Id, @Date, @Status, @PersonId, @VisitId)";
            await connection.ExecuteAsync(sql, guidedVisit);
        }

        public async Task UpdateAsync(Guided_Visit guidedVisit)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE Guided_Visits SET Date = @Date, Status = @Status, PersonId = @PersonId, VisitId = @VisitId WHERE Id = @Id";
            await connection.ExecuteAsync(sql, guidedVisit);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Guided_Visits WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
