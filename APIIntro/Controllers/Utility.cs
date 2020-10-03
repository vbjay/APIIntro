using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System.Linq;

namespace APIIntro.Controllers
{
    [ApiController]
    [Route("api")]
    public class Utility : Controller
    {
        private IConfiguration _config;

        public Utility(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/Config
        [HttpGet("Config", Name = nameof(GetConfig))]
        [Authorize("LocalHostOnly")]
        public IActionResult GetConfig() => Ok(_config.AsEnumerable());
    }
}
