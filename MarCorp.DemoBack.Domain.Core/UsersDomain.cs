using MarCorp.DemoBack.Data.Interface;
using MarCorp.DemoBack.Domain.Interface;
using MarCorp.DemoBack.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarCorp.DemoBack.Domain.Core
{
    public class UsersDomain : IUsersDomain
    {
        private readonly IUsersRepository _usersRepository;
        public UsersDomain(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            return await _usersRepository.AuthenticateAsync(userName, password);
        }
    }
}
