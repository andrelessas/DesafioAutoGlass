using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.CORE.Notifications;
using DesafioAutoGlass.INFRASTRUCTURE.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace DesafioAutoGlass.INFRASTRUCTURE.Persistense.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DesafioAutoGlassContext context)
        {
            _context = context;
            Produto = new ProdutoRepository(context);
            Fornecedor = new FornecedorRepository(context);
        }

        private readonly DesafioAutoGlassContext _context;
        private IDbContextTransaction _transaction;

        public IProdutoRepository Produto { get; }
        public IFornecedorRepository Fornecedor { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (ExcecoesPersonalizadas)
            {
                await _transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}