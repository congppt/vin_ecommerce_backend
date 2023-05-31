using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Account;
using VinEcomViewModel.Global;

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
            if (result is null) return Unauthorized(VinEcomService.Resources.VinEcom.VINECOM_AUTHORIZE_FAILED);
            return Ok(result);
        }
    }
}
