using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly AutoglassContext _context;
        private readonly IUnitOfWork _UoW;
        private readonly DbSet<User> DbSet;
        protected readonly DbConnection _dbConnection;

        public UserRepository(AutoglassContext context, IUnitOfWork UoW)
        {
            _context = context;
            _UoW = UoW;
            DbSet = _context.Set<User>();
            _dbConnection = _context.Database.GetDbConnection();
        }
        public async Task<bool> ValidaUserAsync(string userName, string password)
        {
            var resultado = await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.UserName == userName && c.Password == password);
            return (resultado != null);

            //  return await Task.FromResult<bool>(true);
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await Task.FromResult<IEnumerable<User>>(null);
        }
        public async Task<User> GetIdAsync(int IdUser)
        {
            return await Task.FromResult<User>(null);
        }
    }
}
