using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Account;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> AuthorizeAsync(string phone, string password)
        {
            var vm = await customerService.AuthorizeAsync(phone, password);
            if (vm is null) return Unauthorized();
            return Ok(vm);
        }
    }
}
