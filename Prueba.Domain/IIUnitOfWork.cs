using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Prueba.Domain
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
        IUnitOfWorkRepository Repository();
        IDbContextTransaction BeginTransaction();
    }
}
