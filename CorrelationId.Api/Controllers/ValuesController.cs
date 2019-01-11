using Microsoft.AspNetCore.Mvc;

namespace CorrelationId.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello world!");
        }
    }
}
