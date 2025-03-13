using MarCorp.DemoBack.Application.Interface.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MarCorp.DemoBack.Persistence.Contexts
{
    public class DapperContext : IDapperContext
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
