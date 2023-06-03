using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface;
using VinEcomInterface.IService;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IBaseService baseService ;
        public BaseController(IBaseService baseService)
        {
            this.baseService = baseService;
        }
        [HttpGet("Buildings")]
        public async Task<IActionResult> GetBuildings()
        {
            var buildings = await baseService.GetBuildings();
            if (buildings == null) return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(buildings);
        }
    }
}
