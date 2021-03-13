using System.IO;
using Iac.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Iac.Api.Infrastructure.Factories
{
    public class StackContextFactory : IDesignTimeDbContextFactory<StackContext>
    {
        public StackContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StackContext>();

            optionsBuilder.UseNpgsql(config["ConnectionString"],
                npgsqlOptionsAction: o => o.MigrationsAssembly("Iac.Api"));

            return new StackContext(optionsBuilder.Options);
        }
    }
}