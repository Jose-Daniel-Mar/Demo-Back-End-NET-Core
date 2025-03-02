using Dapper;
using MarCorp.DemoBack.Data.Connections;
using MarCorp.DemoBack.Data.Interface;
using MarCorp.DemoBack.Domain.Models.Entities;
using System.Data;

namespace MarCorp.DemoBack.Data.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IDapperContext _context;
        public CategoriesRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using var connection = _context.Createconnection();
            var query = "SELECT * FROM Categories";
            var categories = await connection.QueryAsync<Category>(query, commandType: CommandType.Text);
            
            return categories;
        }
    }
}
