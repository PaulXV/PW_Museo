using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

namespace PW_Museo.Repositories
{
    public class ShowsRepository
    {
        private readonly string _connectionString;

        public ShowsRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString:db"] ?? throw new InvalidOperationException("Connection string 'db' not found.");
        }

        public async Task<IEnumerable<Show>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Show>("SELECT * FROM Shows");
        }

        public async Task<Show> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Show>("SELECT * FROM Shows WHERE Id = @Id", new { Id = id });
        }

        public async Task CreateAsync(Show show)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Shows (Id, Title, Description, StartDate, EndDate, Status, ImageId, AuthorId) VALUES (@Id, @Title, @Description, @StartDate, @EndDate, @Status, @ImageId, @AuthorId)";
            await connection.ExecuteAsync(sql, show);
        }

        public async Task UpdateAsync(Show show)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE Shows SET Title = @Title, Description = @Description, StartDate = @StartDate, EndDate = @EndDate, Status = @Status, ImageId = @ImageId, AuthorId = @AuthorId WHERE Id = @Id";
            await connection.ExecuteAsync(sql, show);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Shows WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
