using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoGlass.CORE.Interfaces
{
    public interface IUnitOfWork
    {
        public IProdutoRepository Produto { get; }
        public IFornecedorRepository Fornecedor { get; }        
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}