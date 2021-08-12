using System.Threading.Tasks;
using Prueba.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Prueba.Infrastructure.Data.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUnitOfWorkRepository repository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            repository = new UnitOfWorkRepository(this._context);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IUnitOfWorkRepository Repository()
        {
            return this.repository;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
        }
    }
}
