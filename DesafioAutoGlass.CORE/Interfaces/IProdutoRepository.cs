using System.Linq.Expressions;
using DesafioAutoGlass.CORE.Entities;
using DesafioAutoGlass.CORE.Models;

namespace DesafioAutoGlass.CORE.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> GetByIdAsync(int id);
        Task<PaginationResult<Produto>> GetAllAsync(string query, int page);
    }
}