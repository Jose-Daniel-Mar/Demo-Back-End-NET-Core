

using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Domain.Interface
{
    public interface IUsersDomain
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
