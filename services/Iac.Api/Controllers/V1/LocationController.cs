using System;
using System.Net;
using System.Threading.Tasks;
using EventBus.Extensions;
using Iac.Api.Application.Commands;
using Iac.Api.Application.Commands.LocationCommands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Controllers.V1
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        public LocationController(ILogger<LocationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// get a location by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public Task<IActionResult> Get(long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create new location
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post([FromBody] CreateLocationCommand command,[FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CreateLocationCommand, bool>(command, guid);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCancelOrder.GetGenericTypeName(),
                    nameof(requestCancelOrder.Command.Title),
                    requestCancelOrder.Command.Title,
                    requestCancelOrder);

                commandResult = await _mediator.Send(requestCancelOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}