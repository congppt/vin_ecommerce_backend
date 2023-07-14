using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Base;
using VinEcomDomain.Resources;
using VinEcomViewModel.StoreStaff;
using VinEcomService.Service;
using VinEcomAPI.CustomWebAttribute;
using VinEcomDomain.Enum;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]s")]
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
            if (result is null) return Unauthorized(new { message = VinEcom.VINECOM_USER_AUTHORIZE_FAILED });
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] StoreStaffSignUpViewModel vm)
        {
            var validateResult = await staffService.ValidateRegistrationAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            if (await staffService.IsPhoneExistAsync(vm.Phone)) return Conflict(new { message = VinEcom.VINECOM_USER_REGISTER_PHONE_DUPLICATED });
            if (!await staffService.IsStoreExistedAsync(vm.StoreId)) return Conflict(new { message = VinEcom.VINECOM_STORE_NOT_EXIST });
            var result = await staffService.RegisterAsync(vm);
            if (result) return Created("", new { message = VinEcom.VINECOM_USER_REGISTER_SUCCESS });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }

        #region CancelOrder
        [EnumAuthorize(Role.Staff)]
        [HttpPatch("order/cancel/{orderId}")]
        public async Task<IActionResult> CancelOrderAsync(int orderId)
        {
            if (orderId <= 0) return BadRequest();
            //
            var order = await staffService.GetOrderByIdAsync(orderId);
            if (order == null) return NotFound(new { Message = VinEcom.VINECOM_ORDER_NOT_FOUND });
            //
            var result = await staffService.CancelOrderAsync(order);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_ORDER_CANCEL_FAILED });
        }
        #endregion
    }
}
