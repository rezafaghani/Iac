using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iac.Api.Application.Commands.PulumiCommands.Create;
using Iac.Api.Application.Models;
using Iac.Domain.AggregateModels;
using Iac.Domain.SeedWork;
using Iac.Infrastructure.Idempotency;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Application.Commands.StackCommands.Create
{
    public class StackCreateCommandHandler:IRequestHandler<StackCreateCommand,bool>
    {
        private readonly IRepository<StackEntity> _stackRepository;
        private readonly IRepository<NetworkEntity> _networkRepository;
        private readonly IRepository<OsEntity> _osRepository;
        private readonly IMediator _mediator;
        

        public StackCreateCommandHandler(IRepository<StackEntity> stackRepository, IRepository<NetworkEntity> networkRepository, IMediator mediator, IRepository<OsEntity> osRepository)
        {
            _stackRepository = stackRepository;
            _networkRepository = networkRepository;
            _mediator = mediator;
            _osRepository = osRepository;
        }

        public async Task<bool> Handle(StackCreateCommand request, CancellationToken cancellationToken)
        {
            var network=await _networkRepository.GetAll().FirstOrDefaultAsync(x =>
                x.Name.ToLower().Equals(request.Network.Name.ToLower()), cancellationToken);
            if (network == null)
            {
                network=  _networkRepository.Add(new NetworkEntity
                {
                    Hostname = request.Network.Hostname,
                    Ipv4Address = request.Network.Ipv4Address,
                    Ipv6Address = request.Network.Ipv6Address,
                    NetworkType = request.Network.NetworkType,
                    MacAddress = request.Network.MacAddress,
                    DomainName = request.Network.DomainName,
                    Name = request.Network.Name,
                    PrimaryDnsServer = request.Network.PrimaryDnsServer,
                    SecondaryDnsServer = request.Network.SecondaryDnsServer
                });
                await _networkRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }

            _stackRepository.Add(new StackEntity
            {
                OsId = request.OsId,
                StackName = request.StackName,
                StackItems = request.Items.Select(x => new StackItemsEntity
                {

                    NetworkId = network.Id,
                    ImageName = x.ImageName,
                    Title = x.Title,
                    Variables = x.Variables.Any()? x.Variables.Select(y=>new StackItemVariablesEntity
                    {
                        Name = y.Name,
                        Value = y.Value
                    }).ToList():new List<StackItemVariablesEntity>()
                }).ToList()
            });
            await _stackRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            var osDto=await _osRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.OsId, cancellationToken: cancellationToken);
        var result=  await  _mediator.Send(new CreatePulumiCommand
            {
                OsInfo = new OsDto
                {
                    Title = osDto.Title,
                    ImageName = osDto.ImageName,
                    Version = osDto.Version,
                    LocationId = osDto.LocationId
                },
                StackInfo = new StackDto
                {
                    Network = new NetworkDto
                    {
                        Hostname = request.Network.Hostname,
                        Ipv4Address = request.Network.Ipv4Address,
                        Ipv6Address = request.Network.Ipv6Address,
                        NetworkType = request.Network.NetworkType,
                        MacAddress = request.Network.MacAddress,
                        DomainName = request.Network.DomainName,
                        Name = request.Network.Name,
                        PrimaryDnsServer = request.Network.PrimaryDnsServer,
                        SecondaryDnsServer = request.Network.SecondaryDnsServer
                    },
                    OsId = request.OsId,
                    StackName = request.StackName,
                    Items = request.Items.Select(x => new StackItemDto()
                    {

                        ImageName = x.ImageName,
                        Title = x.Title,
                        Variables = x.Variables.Any()
                            ? x.Variables.Select(y => new StackItemVariableDto()
                            {
                                Name = y.Name,
                                Value = y.Value
                            }).ToList()
                            : new List<StackItemVariableDto>()
                    }).ToList()
                }
            }, cancellationToken);

        return result > 0;
        }
    }
    public class CreateStackIdentifiedCommandHandler : IdentifiedCommandHandler<StackCreateCommand, bool>
    {
        public CreateStackIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<StackCreateCommand, bool>> logger)
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for processing order.
        }
    }
}