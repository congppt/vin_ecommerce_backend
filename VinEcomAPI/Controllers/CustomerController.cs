using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Account;

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
        public async Task<IActionResult> AuthorizeAsync(string phone, string password)
        {
            var vm = await customerService.AuthorizeAsync(phone, password);
            if (vm is null) return Unauthorized(VinEcomService.Resources.VinEcom.VinEcom_Authorize_Failed);
            return Ok(vm);
        }
    }
}
