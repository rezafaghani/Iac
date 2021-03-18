using System.Collections.Generic;
using Iac.Api.Application.Models;
using MediatR;

namespace Iac.Api.Application.Commands.StackCommands.Create
{
    public class StackCreateCommand:IRequest<bool>
    {
        public string StackName { get; set; }
        public long OsId { get; set; }
        public List<StackItemDto> Items { get; set; }
        public NetworkDto Network { get; set; }
    }
}