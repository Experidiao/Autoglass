using Autoglass.Application.Interface;
using Autoglass.Application.Shared;
using Autoglass.Domain.Authentication;
using Autoglass.Domain.Handler;
using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Autoglass.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        public UserApplication(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<bool> ValidaUserAsync(string userName, string password)
        {

            return await _userRepository.ValidaUserAsync(userName, password);
        }
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _userRepository.GetAsync();
        }
        public async Task<User> GetIdAsync(int IdUser)
        {
            return await _userRepository.GetIdAsync(IdUser);
        }

        public async Task<string> TokenGenerate(string userName, string password)
        {
            var result = await ValidaUserAsync(userName, password);
            if (result)
                return _tokenGenerator.GetToken(userName, password);
            else 
                return null;
        }

    }
}


