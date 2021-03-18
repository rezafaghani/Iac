using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Iac.Api.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}