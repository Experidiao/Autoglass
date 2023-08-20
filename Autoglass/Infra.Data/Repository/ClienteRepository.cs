using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infra.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AutoglassContext _context;
        private readonly IUnitOfWork _UoW;
        private readonly DbSet<Cliente> DbSet;
        protected readonly DbConnection _dbConnection;

        public ClienteRepository(AutoglassContext context, IUnitOfWork UoW)
        {
            _context = context;
            _UoW = UoW;
            DbSet = _context.Set<Cliente>();
            _dbConnection = _context.Database.GetDbConnection();
        }

        public async Task<Cliente> GetById(int IdCliente)
        {
            return await DbSet.AsNoTracking().FirstAsync(c => c.IdCliente == IdCliente);
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            List<Cliente> teste;
            teste = await DbSet.ToListAsync();
            //teste = await DbSet.AsNoTracking()
            //   .ToListAsync();
            return teste;
        }

        public async Task<int> Create(Cliente cliente)
        {
            DbSet.Add(cliente);
            await _UoW.Commit();
            return Convert.ToInt32(cliente.IdCliente);
        }

        public async Task<bool> Update(Cliente cliente)
        {
            var xCliente = await GetById(cliente.IdCliente);
            if (xCliente is null)
            {
                // adderror();
                return false;
            }
            // _context.Entry(produto).State = EntityState.Modified;

            //  DbSet.  Update(produto).State = EntityState.Modified;
            DbSet.Update(cliente).State = EntityState.Modified;
            //  var resultado = await _context.SaveChangesAsync();
            return await _UoW.Commit();
        }
        public async Task<bool> Delete(int Id)
        {
            var cliente = await GetById(Id);
            if (cliente is null)
            {
                // AddError("Esta produto nao existe.");
                return false;
            }

            DbSet.Remove(cliente).State = EntityState.Deleted;
            //   var resultado = await _context.SaveChangesAsync();
            return await _UoW.Commit();
        }


        public async Task<IEnumerable<Cliente>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura)
        {
            string filtroWhere = "";
            if (!string.IsNullOrEmpty(textoProcura))
            {
                filtroWhere = string.Format(" where nome like '%{0}%'", textoProcura);
            }

            var sql = @$" select * from Tblcliente 
                        {(string.IsNullOrEmpty(filtroWhere) ? null : filtroWhere)}
                          order by {(string.IsNullOrEmpty(ordem) ? "Nome":ordem )}
                         offset (@Pagina - 1) ROWS
                          FETCH NEXT @QtdPorPagina ROWS ONLY";

            var listaCliente = await _dbConnection.QueryAsync<Cliente>(sql, new { Pagina = pagina, QtdPorPagina = qtdePorPagina });
            return listaCliente;
        }
    }
}

