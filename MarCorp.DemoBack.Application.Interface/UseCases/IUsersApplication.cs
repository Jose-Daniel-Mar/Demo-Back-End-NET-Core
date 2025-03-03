using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Support.Common;

namespace MarCorp.DemoBack.Application.Interface.UseCases
{
    public interface IUsersApplication
    {
        Task<Response<UsersDTO>> AuthenticateAsync(string username, string password);
    }
}
