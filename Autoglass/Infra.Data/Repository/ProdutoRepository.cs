using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infra.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AutoglassContext _context;
        private readonly IUnitOfWork _UoW;
        private readonly DbSet<Produto> DbSet;
        protected readonly DbConnection _dbConnection;

        public ProdutoRepository(AutoglassContext context, IUnitOfWork UoW)
        {
            _context = context;
            _UoW = UoW;
            DbSet = _context.Set<Produto>();
            _dbConnection = _context.Database.GetDbConnection();
        }

        public async Task<Produto> GetById(int IdProduto)
        {
                return await DbSet.AsNoTracking().FirstAsync(c => c.IdProduto == IdProduto);
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ProcurarProduto(string ordenarPor, string valorPesquisa,string campoPesquisa)
        {
            
            var sql = "select * from TblProduto";

            if (!string.IsNullOrEmpty(campoPesquisa) && (!string.IsNullOrEmpty(valorPesquisa)))
                sql = string.Format(sql + " where "+ campoPesquisa + " like \'%{0}", valorPesquisa+"%\'");

            if (!string.IsNullOrEmpty(ordenarPor))
                sql = string.Format(sql + " order by {0}", ordenarPor);

            return await _dbConnection.QueryAsync<Produto>(sql, null, commandType: CommandType.Text);
        }


        public async Task<int> Create(Produto produto)
        { 

            DbSet.Add(produto);
            await _UoW.Commit();
            return Convert.ToInt32(produto.IdProduto);
        }

        public async Task<bool> Update(Produto produto)
        {
            var xProduto = await GetById(produto.IdProduto);
            if (xProduto is null)
            {
                // adderror();
                return false;
            }
            // _context.Entry(produto).State = EntityState.Modified;

            //  DbSet.  Update(produto).State = EntityState.Modified;
            DbSet.Update(produto).State = EntityState.Modified;
            //  var resultado = await _context.SaveChangesAsync();
            return await _UoW.Commit();
        }
        public async Task<bool> Delete(int Id)
        {
            var produto = await GetById(Id);
            if (produto is null)
            {
                // AddError("Esta produto nao existe.");
                return false;
            }

            DbSet.Remove(produto).State = EntityState.Deleted;
            //   var resultado = await _context.SaveChangesAsync();
            return await _UoW.Commit();
        }

        public async Task<IEnumerable<Produto>> Page(int pagina, int qtdePorPagina, string ordem, string textoProcura)
        {
            string filtroWhere = "";
            if (!string.IsNullOrEmpty(textoProcura))
            {
                filtroWhere = string.Format(" where Descricao like '%{0}%'", textoProcura);
            }

            var sql = @$" select * from TblProduto 
                        {(string.IsNullOrEmpty(filtroWhere) ? null : filtroWhere)}
                          order by {(string.IsNullOrEmpty(ordem) ? "Descricao" : ordem)}
                         offset (@Pagina - 1) ROWS
                          FETCH NEXT @QtdPorPagina ROWS ONLY";

            var listaProduto = await _dbConnection.QueryAsync<Produto>(sql, new { Pagina = pagina, QtdPorPagina = qtdePorPagina });
            return listaProduto;
        }

    }
}
