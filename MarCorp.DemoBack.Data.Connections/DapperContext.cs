using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MarCorp.DemoBack.Data.Connections
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("NorthwindConnection") ?? throw new InvalidOperationException("Connection string 'NorthwindConnection' not found.");
        }

        public IDbConnection Createconnection() => new SqlConnection(_connectionString);
    }
}
