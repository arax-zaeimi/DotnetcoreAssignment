using Hospitad.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospitad.Application.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {   
        IQueryable<TEntity> GetAll(bool trackChanges = false);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        // Commands
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
    }
}
