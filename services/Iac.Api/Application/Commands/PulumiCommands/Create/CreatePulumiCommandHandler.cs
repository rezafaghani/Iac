using System.Threading;
using System.Threading.Tasks;
using Iac.Api.Application.Models;
using Iac.Api.Infrastructure.Services.PulumiServices;
using MediatR;
using Pulumi;

namespace Iac.Api.Application.Commands.PulumiCommands.Create
{
    public class CreatePulumiCommandHandler:IRequestHandler<CreatePulumiCommand,bool>
    {
        public async Task<bool> Handle(CreatePulumiCommand request, CancellationToken cancellationToken)
        {
            var redisLeader = new ServiceDeployment("redis-leader", new ServiceDeploymentArgs
            {
                Image = "redis",
                Ports = {6379}
            });
            return true;

        }
    }
}