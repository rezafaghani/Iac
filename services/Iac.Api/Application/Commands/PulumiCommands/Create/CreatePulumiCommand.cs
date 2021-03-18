using Iac.Api.Application.Models;
using MediatR;

namespace Iac.Api.Application.Commands.PulumiCommands.Create
{
    public class CreatePulumiCommand:IRequest<int>
    {
        public OsDto OsInfo { get; set; }

        public StackDto StackInfo { get; set; }
    }
}