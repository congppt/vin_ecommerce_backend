using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Base;
using VinEcomViewModel.Customer;
using static VinEcomService.Resources.VinEcom;

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
        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
        {
            var result = await customerService.AuthorizeAsync(vm);
            if (result is null) return Unauthorized(VINECOM_USER_AUTHORIZE_FAILED);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CustomerSignUpViewModel vm)
        {
            if (await customerService.IsPhoneExist(vm.Phone)) return BadRequest(VINECOM_USER_REGISTER_PHONE_DUPLICATED);
            var result = await customerService.RegisterAsync(vm);
            if (result) return Created("", VINECOM_USER_REGISTER_SUCCESS);
            return StatusCode(StatusCodes.Status500InternalServerError, VINECOM_USER_REGISTER_INTERNAL_FAILED);
        }
    }
}
