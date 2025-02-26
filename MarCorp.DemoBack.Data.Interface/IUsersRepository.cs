using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Data.Interface
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
