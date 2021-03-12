using System.Reflection;
using Autofac;
using EventBus.Abstractions;
using Iac.Api.Application.Commands.OsCommands;
using Iac.Domain.SeedWork;
using Iac.Infrastructure.Idempotency;
using Iac.Infrastructure.Repositories;

namespace Iac.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

     
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.RegisterType<RequestManager>()
                .As<IRequestManager>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateOsCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}