using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Support.Common;

namespace MarCorp.DemoBack.Application.Interface
{
    public interface IUsersApplication
    {
        Response<UsersDTO> Authenticate(string username, string password);
    }
}
