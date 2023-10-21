using Autoglass.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Domain.Authentication
{
    public interface ITokenGenerator
    {
       string GetToken(string userName, string password);
    }
}
