using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.Interface
{
    public interface IUserApplication 
    {
        Task<IEnumerable<User>> GetAsync ();
        Task<User> GetIdAsync (int IdUser);
        Task<bool> ValidaUserAsync(string userName, string password );

        Task<string> TokenGenerate(string userName, string password);

    }
}
