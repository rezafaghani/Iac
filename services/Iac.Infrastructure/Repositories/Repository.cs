using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iac.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Iac.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
       where T : Entity,IAggregateRoot
    {
        private readonly StackContext _context;

        public Repository(StackContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public T Add(T entity)
        {
            entity.SetCreateDateTime();
            return _context.Set<T>()
                .Add(entity)
                .Entity;
        }

        public async Task AddRange(List<T> entity)
        {
            await _context.Set<T>().AddRangeAsync(entity);

        }

        public T Update(T entity)
        {
            entity.SetUpdateDateTime();
            return _context.Set<T>()
                .Update(entity)
                .Entity;
        }

        public T Delete(T entity)
        {
            entity.SetDeleteDateTime();
            entity.SetDeleted();
            return _context.Set<T>()
                .Update(entity)
                .Entity;
        }

        public IQueryable<T> GetAll()
        {
            var query = _context.Set<T>().Where(x => !x.Deleted).AsQueryable();
            return query;
        }

        public async Task<T> FindAsync(T entity)
        {
            var exitEntity = await _context.Set<T>()
                .Where(b => b.Id == entity.Id && !b.Deleted)
                .SingleOrDefaultAsync();

            return exitEntity;
        }
    }
}