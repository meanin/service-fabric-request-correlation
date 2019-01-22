using System.Threading.Tasks;
using CorrelationId.Contract;
using Microsoft.AspNetCore.Mvc;

namespace CorrelationId.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IService1 _service1;
        private readonly IService2 _service2;

        public ValuesController(
            IService1 service1, 
            IService2 service2)
        {
            _service1 = service1;
            _service2 = service2;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello world!");
        }
        
        [HttpGet]
        [Route("s1ok")]
        public async Task<IActionResult> S1Ok()
        {
            var result = await _service1.Ok();
            return Ok(result);
        }

        [HttpGet]
        [Route("s1nok")]
        public async Task<IActionResult> S1Nok()
        {
            var result = await _service1.Nok();
            return Ok(result);
        }

        [HttpGet]
        [Route("s2ok")]
        public async Task<IActionResult> S2Ok()
        {
            var result = await _service2.Ok();
            return Ok(result);
        }

        [HttpGet]
        [Route("s2nok")]
        public async Task<IActionResult> S2Nok()
        {
            var result = await _service2.Nok();
            return Ok(result);
        }

        [HttpGet]
        [Route("s1s2ok")]
        public async Task<IActionResult> S1S2Ok()
        {
            var result = await _service1.Service2Ok();
            return Ok(result);
        }

        [HttpGet]
        [Route("s1s2nok")]
        public async Task<IActionResult> S1S2Nok()
        {
            var result = await _service1.Service2Nok();
            return Ok(result);
        }
    }
}
