using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;
using IdentityApi.ViewModels.Account;

namespace IdentityApi.Data.Interfaces
{
    public interface IAccountRepository
    { 
        Task<string> GenerateJWTToken(User user);
        Task<InfoAbutUserViewModel> GetInfoAboutUserByIdAsync(string Id);
    }
}
