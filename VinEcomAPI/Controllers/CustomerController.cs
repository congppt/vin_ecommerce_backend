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
    }
}
