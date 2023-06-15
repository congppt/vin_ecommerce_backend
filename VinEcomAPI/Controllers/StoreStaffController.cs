using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Base;
using VinEcomDomain.Resources;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreStaffController : ControllerBase
    {
        private readonly IStoreStaffService staffService;
        public StoreStaffController(IStoreStaffService staffService)
        {
            this.staffService = staffService;
        }
        [HttpPost("Authorize")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
        {
            var result = await staffService.AuthorizeAsync(vm);
            if (result is null) return Unauthorized(new { message = VinEcom.VINECOM_USER_AUTHORIZE_FAILED });
            return Ok(result);
        }
    }
}
