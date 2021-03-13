using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Iac.Domain.AggregateModels;
using Iac.Domain.SeedWork;
using Iac.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace Iac.Infrastructure
{
    public class StackContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "stack";
        public DbSet<OsEntity> Oses { get; set; }
        public DbSet<StackEntity> Stacks { get; set; }
        public DbSet<StackItemsEntity> StackItems { get; set; }
        public DbSet<StackItemVariablesEntity> StackItemVariables { get; set; }
       

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public StackContext(DbContextOptions<StackContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public StackContext(DbContextOptions<StackContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NetworkEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StackEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StackItemsEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StackItemVariablesEntityTypeConfiguration());
           
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
    public class StackContextDesignFactory : IDesignTimeDbContextFactory<StackContext>
    {
        public StackContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StackContext>()
                .UseNpgsql("Server=.;Initial Catalog=Microsoft.eShopOnContainers.Services.OrderingDb;Integrated Security=true");

            return new StackContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}