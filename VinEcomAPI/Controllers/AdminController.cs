using Microsoft.AspNetCore.Mvc;
using VinEcomDomain.Resources;
using VinEcomInterface.IService;
using VinEcomViewModel.Base;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService userService;

        public AdminController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
        {
            var result = await userService.AdminAuthorizeAsync(vm);
            if (result is null) return Unauthorized(new { message = VinEcom.VINECOM_USER_AUTHORIZE_FAILED });
            return Ok(result);
        }
    }
}
