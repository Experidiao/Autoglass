using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infra.Data.Repository
{
    public class ClienteXProdutoRepository : IClienteXProdutoRepository
    {
        private readonly AutoglassContext _context;
        private readonly IUnitOfWork _UoW;
        private readonly DbSet<ClienteXProduto> DbSet;
        protected readonly DbConnection _dbConnection;
        public ClienteXProdutoRepository(AutoglassContext context, IUnitOfWork UoW)
        {
            _context = context;
            _UoW = UoW;
            DbSet = _context.Set<ClienteXProduto>();
            _dbConnection = _context.Database.GetDbConnection();
        }

        public async Task<ClienteXProduto> GetById(int IdClienteXProduto)
        {
            return await DbSet.AsNoTracking().FirstAsync(c => c.IdClienteXProduto == IdClienteXProduto);
        }

        public async Task<IEnumerable<ClienteXProduto>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<int> Create(ClienteXProduto clienteXproduto)
        {
            DbSet.Add(clienteXproduto);
            await _UoW.Commit();
            return Convert.ToInt32(clienteXproduto.IdClienteXProduto);
        }

        public async Task<bool> Update(ClienteXProduto clienteXproduto)
        {
            var xClienteXProduto = await GetById(clienteXproduto.IdClienteXProduto);
            if (xClienteXProduto is null)
            {
                // adderror();
                return false;
            }
            // _context.Entry(produto).State = EntityState.Modified;

            //  DbSet.  Update(produto).State = EntityState.Modified;
            DbSet.Update(xClienteXProduto).State = EntityState.Modified;
            //  var resultado = await _context.SaveChangesAsync();
            return await _UoW.Commit();
        }
        public async Task<bool> Delete(int Id)
        {
            var clienteXproduto = await GetById(Id);
            if (clienteXproduto is null)
            {
                // AddError("Esta produto nao existe.");
                return false;
            }

            DbSet.Remove(clienteXproduto).State = EntityState.Deleted;
            //   var resultado = await _context.SaveChangesAsync();
            return await _UoW.Commit();
        }

        // Usando o DAPPER
        public async Task<IEnumerable<ClienteXProduto>> ObterProdutoDoCliente(int idCliente)
        {
            var sql = @" Select * from TblClienteXProduto pc
                    join Tblcliente cli on cli.idCliente = pc.IdCliente 
                    where cli.IdCliente = @IdCliente ";

            return await _dbConnection.QueryAsync<ClienteXProduto>(sql, new { @IdCliente = idCliente });
        }

        // Usando o SQL.DATA
        //public async Task<IEnumerable<ClienteXProduto>> ObterProdutoDoClienteNativo(int idCliente)
        //{
        //    using (var conexao = SqlConnection(connectionString))
        //    { 
        //    using (var command = new SqlCommand())
        //    {
        //        command.Connection = conexao;
        //        command.CommandType = CommandType.Text;
        //        command.CommandText = "Select * from TBlClienteXProduto ";
        //        var reader = command.ExecuteReader();
        //    }
        //    }
        //}
        public async Task<IEnumerable<ClienteXProduto>> ObterProdutoDoClienteEntity(int idCliente)
        {
            // LEZY loading. Precisa instalar o pacote Microsoft.EntityFrameworkCore.Proxies
            //var sql = from e in _context.TblClienteXProduto
            //          where e.IdCliente == idCliente
            //          select e;
            //var clienteXProduto = await sql.ToListAsync();
            //return (IEnumerable<ClienteXProduto>)clienteXProduto;

            // EAGER loading. Precisa incluir o Incluide da tabela

            var clienteXProduto = await _context.TblClienteXProduto
                .Include(c => c.produtos)
                .Where(x => x.IdCliente == idCliente)
                .ToListAsync();
            return clienteXProduto;
        }

        public async Task<IEnumerable<ClienteXProduto>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura)
        {
            string filtroWhere = "";
            if (!string.IsNullOrEmpty(textoProcura))
            {
                filtroWhere = string.Format(" where Descricao like '%{0}%'", textoProcura);
            }

            var sql = @$" select cp.IdClienteXProduto,produto.IdProduto,produto.DescricaoFornecedor,produto.Descricao,
                          produto.CodigoFornecedor
                          from TblClienteXProduto  cp
                          inner join TblProduto produto on produto.idProduto = cp.idProduto
                        {(string.IsNullOrEmpty(filtroWhere) ? null : filtroWhere)}
                          order by {(string.IsNullOrEmpty(ordem) ? "Descricao" : ordem)}
                         offset (@Pagina - 1) ROWS
                          FETCH NEXT @QtdPorPagina ROWS ONLY";

            var listaClienteXProduto = await _dbConnection.QueryAsync<ClienteXProduto,Produto, ClienteXProduto>(@sql,
                map :(clienteXproduto,produto) =>
                {
                    clienteXproduto.produtos = produto;
                    return clienteXproduto;
                },
                new { Pagina = pagina, QtdPorPagina = qtdePorPagina },splitOn:"IdProdutoXCliente,IdProduto");
            return listaClienteXProduto;
        }

    }
}

