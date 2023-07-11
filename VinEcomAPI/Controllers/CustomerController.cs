using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Base;
using VinEcomViewModel.Customer;
using VinEcomDomain.Resources;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        [HttpPost("Authorize")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
        {
            var result = await customerService.AuthorizeAsync(vm);
            if (result is null) return Unauthorized(new { message = VinEcom.VINECOM_USER_AUTHORIZE_FAILED });
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CustomerSignUpViewModel vm)
        {
            var validateResult = await customerService.ValidateRegistrationAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            } 
            if (await customerService.IsPhoneExistAsync(vm.Phone)) return Conflict(new { message = VinEcom.VINECOM_USER_REGISTER_PHONE_DUPLICATED });
            if (!await customerService.IsBuildingExistedAsync(vm.BuildingId)) return Conflict(new { message = VinEcom.VINECOM_BUILDING_NOT_EXIST});
            var result = await customerService.RegisterAsync(vm);
            if (result) return Created("", new { message = VinEcom.VINECOM_USER_REGISTER_SUCCESS });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }

        [HttpGet("Customers/{id?}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await customerService.GetCustomerByIdAsync(id);
            if (result is not null) return Ok(result);
            return NotFound();
        }

        [HttpGet("Customers")]
        public async Task<IActionResult> GetCustomerPages(int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest(new { message = VinEcom.VINECOM_PAGE_INDEX_ERROR });
            if (pageSize <= 0) return BadRequest(new { message = VinEcom.VINECOM_PAGE_SIZE_ERROR });
            var result = await customerService.GetCustomerPagesAsync(pageIndex, pageSize);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonalInfo()
        {
            var result = await customerService.GetPersonalInfoAsync();
            return Ok(result);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdatePersonalBasicInfoAsync([FromBody] CustomerUpdateBasicViewModel vm)
        {
            var validateResult = await customerService.ValidateUpdateBasicAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            var result = await customerService.UpdateBasicInfoAsync(vm);
            if (result) return Ok(new { message = VinEcom.VINECOM_UPDATE_SUCCESS});
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }
        [HttpPatch("ChangePassword")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordViewModel vm)
        {
            var validateResult = await customerService.ValidateUpdatePasswordAsync(vm); 
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            if (!await customerService.IsCorrectCurrentPasswordAsync(vm)) return Conflict(new { message = VinEcom.VINECOM_CURRENT_PASSWORD_INCORRECT });
            var result = await customerService.UpdatePasswordAsync(vm);
            if (result) return Ok(new { message = VinEcom.VINECOM_UPDATE_SUCCESS });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }
    }
}
