using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Base;
using static VinEcomService.Resources.VinEcom;

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
        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
        {
            var result = await staffService.AuthorizeAsync(vm);
            if (result is null) return Unauthorized(VINECOM_USER_AUTHORIZE_FAILED);
            return Ok(result);
        }
    }
}
