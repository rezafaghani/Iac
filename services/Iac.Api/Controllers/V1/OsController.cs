using System;
using System.Net;
using System.Threading.Tasks;
using EventBus.Extensions;
using Iac.Api.Application.Commands;
using Iac.Api.Application.Commands.OsCommands.Create;
using Iac.Api.Application.Queries.OsQuery.Dto;
using Iac.Api.Infrastructure.ApiResults;
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
    public class OsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OsController> _logger;

        public OsController(IMediator mediator, ILogger<OsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// get info of a OS that exist 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet("{id}",Name = "getOs")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(long id)
        {
            return Ok();
        }
        
        /// <summary>
        /// get list of os depend on search item
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(PagedResult<object>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PagedResult<object>),(int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] OsFilterDto filter)
        {
            return Ok();
        }

        /// <summary>
        /// create new os
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOs([FromBody] CreateOsCommand command,[FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;

            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CreateOsCommand, bool>(command, guid);

                _logger.LogInformation(
                    "----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    requestCancelOrder.GetGenericTypeName(),
                    nameof(requestCancelOrder.Command.Title),
                    requestCancelOrder.Command.ImageName,
                    requestCancelOrder);

                commandResult = await _mediator.Send(requestCancelOrder);
            }

            if (!commandResult)
            {
                return BadRequest();
            }
            return Ok();
           
        }
        /// <summary>
        /// update current os
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateOs(long id,[FromBody] OsFilterDto filter)
        {
            return Ok();
        }
        /// <summary>
        /// delete exist os
        /// before delete checked that has not any item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteOs(long id)
        {
            return Ok();
        }
    }
}