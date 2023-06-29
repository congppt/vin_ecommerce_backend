using Microsoft.AspNetCore.Mvc;
using VinEcomDomain.Model;
using VinEcomInterface.IService;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            this.buildingService = buildingService;
        }

        [HttpGet("Buildings/{id?}")]
        public async Task<IActionResult> GetBuildingById(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await buildingService.GetBuildingById(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
