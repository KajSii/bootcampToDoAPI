using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootcamp.ToDoList.Backend.Entities.Domain;

namespace Visma.Bootcamp.eShop.ApplicationCore.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(User User, string password);
        Task<string> Login(string UserName, string password);
        Task<bool> UserExists(string UserName);
    }
}