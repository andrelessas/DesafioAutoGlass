using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DesafioAutoGlass.CORE.Interfaces
{
    public interface IRepository<TEntity>
    {
        void Update(TEntity entity);
        void Insert(TEntity entity);
    }
}