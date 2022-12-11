
using DesafioAutoGlass.CORE.Interfaces;
using DesafioAutoGlass.INFRASTRUCTURE.Context;

namespace DesafioAutoGlass.INFRASTRUCTURE.Persistense.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DesafioAutoGlassContext _context;
        public Repository(DesafioAutoGlassContext context)
        {
            _context = context;
        }
        public void Insert(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}