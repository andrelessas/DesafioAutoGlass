using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Models;
using DesafioAutoGlass.CORE.Utils;
using DesafioAutoGlass.INFRASTRUCTURE.Context;
using DesafioAutoGlass.INFRASTRUCTURE.Persistense.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DesafioAutoGlass.INFRASTRUCTURE.Persistense.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private const int PAGE_SIZE = 10;
        public ProdutoRepository(DesafioAutoGlassContext context)
            : base(context)
        {}
        public async Task<PaginationResult<Produto>> GetAllAsync(string query, int page)
        {
            IQueryable<Produto> produto = _context.Produto;

            if (!String.IsNullOrWhiteSpace(query))
                produto = produto.Where(x => x.Descricao.Contains(query) || x.Status == query);
            else
                produto = produto.Where(x => x.Status == ConstStatus.ATIVO);

            return await produto
                .AsNoTracking()
                .Include(x => x.Fornecedor)
                .GetPagination(page,PAGE_SIZE);
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _context.Produto
                .Include(x => x.Fornecedor)
                .AsNoTracking()
                .Where(x=>x.Status == ConstStatus.ATIVO)
                .FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}