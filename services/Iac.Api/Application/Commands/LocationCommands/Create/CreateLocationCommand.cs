using MediatR;

namespace Iac.Api.Application.Commands.LocationCommands.Create
{
    public class CreateLocationCommand :IRequest<bool>
    {
        public string Title { get; set; }
    }
}