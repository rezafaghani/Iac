using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Iac.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class NetworkController : Controller
    {
        // GET
        public Task<IActionResult> Index()
        {
            throw new NotImplementedException();
        }
    }
}