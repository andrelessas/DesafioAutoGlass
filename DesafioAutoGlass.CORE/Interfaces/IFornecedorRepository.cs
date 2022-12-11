using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Models;

namespace DesafioAutoGlass.CORE.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<int> MaxIdAsync();
        Task<Fornecedor> GetByIdAsync(int id);
        Task<PaginationResult<Fornecedor>> GetAllAsync(string query, int page);
    }
}