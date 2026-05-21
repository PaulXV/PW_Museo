using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

namespace PW_Museo.Repositories
{
    public class TicketsRepository
    {
        private readonly string _connectionString;

        public TicketsRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:db"] ?? throw new InvalidOperationException("Connection string 'db' not found.");
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Ticket>("SELECT * FROM Tickets");
        }

        public async Task<Ticket?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Ticket>("SELECT * FROM Tickets WHERE Id = @Id", new { Id = id });
        }

        public async Task CreateAsync(Ticket ticket)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Tickets (Id, Type, Quantity, TotalPrice, PurchaseDate, PersonId, ShowId, VisitId) VALUES (@Id, @Type, @Quantity, @TotalPrice, @PurchaseDate, @PersonId, @ShowId, @VisitId)";
            await connection.ExecuteAsync(sql, ticket);
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE Tickets SET Type = @Type, Quantity = @Quantity, TotalPrice = @TotalPrice, PurchaseDate = @PurchaseDate, PersonId = @PersonId, ShowId = @ShowId, VisitId = @VisitId WHERE Id = @Id";
            await connection.ExecuteAsync(sql, ticket);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Tickets WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
