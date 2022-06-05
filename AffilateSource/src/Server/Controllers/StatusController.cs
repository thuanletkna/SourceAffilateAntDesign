using AffilateSource.Data.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AffilateSource.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusServices _statusServices;
        public StatusController(IStatusServices statusServices)
        {
            _statusServices = statusServices;
        }
        [HttpGet("GetListSatatus")]
        public async Task<IActionResult> GetListSatatus()
        {
            var status =await _statusServices.GetListSatatus();
            return Ok(status);
        }
    }
}
