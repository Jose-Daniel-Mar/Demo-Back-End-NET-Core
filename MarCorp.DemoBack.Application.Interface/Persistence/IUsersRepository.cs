﻿using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Application.Interface.Persistence
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
