using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomDomain.Enum;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.UtilityMethod;

namespace VinEcomAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IBaseService baseService;
        public BaseController(IBaseService baseService)
        {
            this.baseService = baseService;
        }
        [HttpGet("buildings")]
        public async Task<IActionResult> GetBuildingsAsync()
        {
            var buildings = await baseService.GetBuildingsAsync();
            if (buildings == null) return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(buildings);
        }
        [HttpGet("store-categories")]
        public IActionResult GetStoreCategories()
        {
            var dict = typeof(StoreCategory).GetEnumDictionary(val => ((StoreCategory)val).GetDisplayName());
            return Ok(dict);
        }
        [HttpGet("product-categories")]
        public IActionResult GetProductCategories()
        {
            var dict = typeof(ProductCategory).GetEnumDictionary(val => ((ProductCategory)val).GetDisplayName());
            return Ok(dict);
        }
    }
}
