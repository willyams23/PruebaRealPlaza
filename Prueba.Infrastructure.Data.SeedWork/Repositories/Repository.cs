using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Prueba.Domain;
using Prueba.Domain.Specifications;

namespace Prueba.Infrastructure.Data.SeedWork.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        protected readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public TEntity Get(decimal id)
        {
            return _entities.Find(id);
        }

        public TEntity Get(string id)
        {
            return _entities.Find(id);
        }
        public TEntity Get(long id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public async Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec)
        {
            return await _entities.Where(spec.SpecExpression).ToListAsync();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public TEntity SingleOrDefault(ISpecification<TEntity> spec)
        {
            return _entities.SingleOrDefault(spec.SpecExpression);
        }

        public async Task<TEntity> SingleOrDefaultAsync(ISpecification<TEntity> spec)
        {
            return await _entities.SingleOrDefaultAsync(spec.SpecExpression);
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity> spec)
        {
            return await _entities.FirstOrDefaultAsync(spec.SpecExpression);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public void Modify(TEntity item)
        {
            _entities.Update(item);
        }

        public IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            if (ascending)
            {
                return _entities.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return _entities.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        public async Task<List<TEntity>> GetPagedAsync<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, bool>> whereExpression,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            if (ascending)
            {
                return await _entities.Where(whereExpression).OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync();
            }
            else
            {
                return await _entities.Where(whereExpression).OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount).ToListAsync();
            }
        }

    }
}