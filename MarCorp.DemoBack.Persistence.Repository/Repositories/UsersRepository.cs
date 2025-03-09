using Dapper;
using MarCorp.DemoBack.Application.Interface.Persistence;
using MarCorp.DemoBack.Domain.Models.Entities;
using System.Data;

namespace MarCorp.DemoBack.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDapperContext _context;
        public UsersRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            using (var connection = _context.Createconnection())
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);
                parameters.Add("Password", password);

                var user = await connection.QuerySingleAsync<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public bool Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public User Get(string Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}