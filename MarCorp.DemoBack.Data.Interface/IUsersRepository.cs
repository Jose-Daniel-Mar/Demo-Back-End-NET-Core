using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Data.Interface
{
    public interface IUsersRepository
    {
        User Authenticate(string username, string password);
    }
}
