using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoglass.Domain.Interfaces;
using Autoglass.Infra.Data.Context;

namespace Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutoglassContext _context;

        public UnitOfWork(AutoglassContext context) => _context = context;
        public async Task<bool> Commit()
        {
            var success = (await _context.SaveChangesAsync()) > 0;
            return success;
        }
    }
}
