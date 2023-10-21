using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Domain.Interfaces
{
    public  interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetIdAsync(int IdUser);
        Task<bool> ValidaUserAsync(string userName, string password);
    }
}
