

using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Domain.Interface
{
    public interface IUsersDomain
    {
        User Authenticate(string username, string password);
    }
}
