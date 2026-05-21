using Dapper;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;


namespace PW_Museo.Services
{
    public class UserDA
    {
        private readonly IConfiguration _configuration;
        private readonly String _dbConnectionString;


        public UserDA(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnectionString = _configuration.GetConnectionString("db")
                ?? throw new InvalidOperationException("Connection string 'db' not found.");
        }


        public async Task<User?> GetUserById(Guid id)
        {
            var connection = new Microsoft.Data.SqlClient.SqlConnection(_dbConnectionString);
            const string query = """
            SELECT TOP (1000) [uid]
                ,[name]
                ,[surname]
                ,[email]
                ,[password]
                ,[isadmin]
            FROM [dbo].[User]
            WHERE uid = @Id
            """;
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }

    }
}
