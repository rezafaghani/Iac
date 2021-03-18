using System;
using System.Net;
using System.Threading.Tasks;
using EventBus.Extensions;
using Iac.Api.Application.Commands;
using Iac.Api.Application.Commands.StackCommands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Iac.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StackController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StackController> _logger;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>

        public StackController(IMediator mediator, ILogger<StackController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
      /// get stack and items
      /// </summary>
      /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(long id)
        {
            return Ok();
        }

      /// <summary>
      /// get stack and items
      /// </summary>
      /// <param name="command"></param>
      /// <param name="requestId"></param>
      /// <returns></returns>
      [HttpPost()]
      [ProducesResponseType((int)HttpStatusCode.OK)]
      [ProducesResponseType((int)HttpStatusCode.BadRequest)]
      public async Task<IActionResult> Post([FromBody] StackCreateCommand command,[FromHeader(Name = "x-requestid")] string requestId)
      {
          bool commandResult = false;

          if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
          {
              var requestCancelOrder = new IdentifiedCommand<StackCreateCommand, bool>(command, guid);

              _logger.LogInformation(
                  "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                  requestCancelOrder.GetGenericTypeName(),
                  nameof(requestCancelOrder.Command.StackName),
                  requestCancelOrder.Command.StackName,
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