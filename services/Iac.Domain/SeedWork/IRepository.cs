using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iac.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        T Add(T entity, long createdBy = 0);
        Task AddRange(List<T> entity);
        T Update(T entity, long? updateBy = null);
        T Delete(T entity, long? deleteBy = null);
        IQueryable<T> GetAll();
        Task<T> FindAsync(T entity);
    }
}