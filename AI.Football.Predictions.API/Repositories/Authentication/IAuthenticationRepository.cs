using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.API.Models;

namespace AI.Football.Predictions.API.Repositories.Authentication
{
    public interface IAuthenticationRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string login, string password);
        Task<bool> UserExists(string login);
    }
}