using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Prueba.Domain.Specifications;
using Prueba.Infrastructure.Crosscutting;

namespace Prueba.Domain
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        TEntity Get(decimal id);
        TEntity Get(string id);
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(ISpecification<TEntity> spec);
        Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> spec);
        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Modify(TEntity item);

        IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending);

        Task<List<TEntity>> GetPagedAsync<KProperty>(int pageIndex, int pageCount,
        Expression<Func<TEntity, bool>> whereExpression,
        Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending);
    }
}
