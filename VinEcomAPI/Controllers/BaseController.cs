using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomAPI.CustomWebAttribute;
using VinEcomDomain.Enum;
using VinEcomDomain.Resources;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomService.Service;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Base;

namespace VinEcomAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IBaseService baseService;
        private readonly IUserService userService;
        public BaseController(IBaseService baseService, IUserService userService)
        {
            this.baseService = baseService;
            this.userService = userService;
        }
        [HttpGet("buildings")]
        public async Task<IActionResult> GetBuildingsAsync()
        {
            var buildings = await baseService.GetBuildingsAsync();
            if (buildings == null) return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok(buildings);
        }

        [HttpGet("buildings/{id?}")]
        public async Task<IActionResult> GetBuildingById(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await baseService.GetBuildingById(id);
            if (result is null) return NotFound();
            return Ok(result);
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
        [EnumAuthorize(Role.Customer | Role.Staff | Role.Shipper)]
        [HttpPatch("password")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordViewModel vm)
        {
            var validateResult = await userService.ValidateUpdatePasswordAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            if (!await userService.IsCorrectCurrentPasswordAsync(vm)) return Conflict(new { message = VinEcom.VINECOM_CURRENT_PASSWORD_INCORRECT });
            var result = await userService.UpdatePasswordAsync(vm);
            if (result) return Ok(new { message = VinEcom.VINECOM_UPDATE_SUCCESS });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }
    }
}
