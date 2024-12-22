using Dapper;
using MarCorp.DemoBack.Data.Connections;
using MarCorp.DemoBack.Data.Interface;
using MarCorp.DemoBack.Domain.Models.Entities;
using System.Data;

namespace MarCorp.DemoBack.Data.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _context;
        public UsersRepository(DapperContext context)
        {
            _context = context;
        }
        public User Authenticate(string userName, string password)
        {
            using (var connection = _context.Createconnection())
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);
                parameters.Add("Password", password);

                var user = connection.QuerySingle<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
    }
}