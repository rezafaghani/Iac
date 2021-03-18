using System.Runtime.Serialization;
using MediatR;

namespace Iac.Api.Application.Commands.OsCommands.Create
{
    public class CreateOsCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Version { get; set; }
        [DataMember]
        public string UserId { get; private set; }

        [DataMember]
        public string UserName { get; private set; }

        public long LocationId { get; set; }
    }
}