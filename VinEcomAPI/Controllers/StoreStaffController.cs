using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Global;
using static VinEcomService.Resources.VinEcom;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreStaffController : ControllerBase
    {
        private readonly IStoreStaffService customerService;
        public StoreStaffController(IStoreStaffService staffService)
        {
            this.customerService = staffService;
        }
        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
        {
            var result = await customerService.AuthorizeAsync(vm);
            if (result is null) return Unauthorized(VINECOM_AUTHORIZE_FAILED);
            return Ok(result);
        }
    }
}
