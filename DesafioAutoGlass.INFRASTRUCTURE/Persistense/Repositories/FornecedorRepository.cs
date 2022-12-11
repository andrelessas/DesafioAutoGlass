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
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        private const int PAGE_SIZE = 10;
        public FornecedorRepository(DesafioAutoGlassContext context)
            : base(context)
        { }
        public async Task<PaginationResult<Fornecedor>> GetAllAsync(string query, int page)
        {
            IQueryable<Fornecedor> fornecedor = _context.Fornecedor;
            
            if (!String.IsNullOrWhiteSpace(query))
                fornecedor = fornecedor.Where(x => x.Descricao.Contains(query) || x.CNPJ.Contains(query) || x.Status == query);
            else
                fornecedor = fornecedor.Where(x => x.Status == ConstStatus.ATIVO);

            return await fornecedor
                .AsNoTracking()                
                .Include(x => x.Produto)
                .GetPagination(page,PAGE_SIZE);
        }

        public async Task<Fornecedor> GetByIdAsync(int id)
        {
            return await _context.Fornecedor
                .AsNoTracking()
                .Where(x => x.Status == ConstStatus.ATIVO)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> MaxIdAsync()
        {
            return await _context.Fornecedor
                .Where(x => x.Status == ConstStatus.ATIVO)
                .Select(x => x.Id)
                .DefaultIfEmpty()
                .MaxAsync();
        }
    }
}