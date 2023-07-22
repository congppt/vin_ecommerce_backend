using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomAPI.CustomWebAttribute;
using VinEcomDomain.Resources;
using VinEcomInterface.IService;
using VinEcomRepository;
using VinEcomService.Service;
using VinEcomViewModel.Base;
using VinEcomViewModel.Shipper;
using VinEcomDomain.Enum;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService shipperService;
        public ShipperController(IShipperService shipperService)
        {
            this.shipperService = shipperService;
        }
        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] SignInViewModel vm)
        {
            var result = await shipperService.AuthorizeAsync(vm);
            if (result is null) return Unauthorized(new { message = VinEcom.VINECOM_USER_AUTHORIZE_FAILED });
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] ShipperSignUpViewModel vm)
        {
            var validateResult = await shipperService.ValidateRegistrationAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            if (await shipperService.IsPhoneExistAsync(vm.Phone)) return Conflict(new { message = VinEcom.VINECOM_USER_REGISTER_PHONE_DUPLICATED });
            var result = await shipperService.RegisterAsync(vm);
            if (result) return Created("", new { message = VinEcom.VINECOM_USER_REGISTER_SUCCESS });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetListAvailableShipper()
        {
            var result = await shipperService.GetShippersAvailableAsync();
            return Ok(result);
        }
        [EnumAuthorize(Role.Shipper)]
        [HttpPut("working-status")]
        public async Task<IActionResult> ChangeWorkingStatus()
        {
            var result = await shipperService.ChangeWorkingStatusAsync();
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_STORE_CHANGE_STATUS_ERROR });
        }
        [EnumAuthorize(Role.Shipper)]
        [HttpPatch("finish-order")]
        public async Task<IActionResult> FinishedOrder()
        {
            var result = await shipperService.OrderDeliveredAsync();
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_SHIPPER_FINISH_ORDER_FAILED });
        }
        [EnumAuthorize(Role.Shipper)]
        [HttpGet("delivered-order")]
        public async Task<IActionResult> GetDeliveredList()
        {
            var result = await shipperService.GetDeliveredListAsync();
            if (result is null) return NotFound();
            return Ok(result);
        }

        #region ReceiveOrder
        [EnumAuthorize(Role.Shipper)]
        [HttpPut("receive-order")]
        public async Task<IActionResult> ReceiveOrder(int orderId)
        {
            if (orderId <= 0) return BadRequest();
            var result = await shipperService.ReceiveOrderAsync(orderId);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_ORDER_ASSIGN_SHIPPER_FAILED });
        }
        #endregion
        [EnumAuthorize(Role.Shipper)]
        [HttpPut("info")]
        public async Task<IActionResult> UpdateBasicInfoAsync(ShipperUpdateBasicViewModel vm)
        {
            var validateResult = await shipperService.ValidateUpdateBasicAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            if (await shipperService.UpdateBasicAsync(vm)) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }

        [EnumAuthorize(Role.Shipper)]
        [HttpGet("info")]
        public async Task<IActionResult> GetShipperInfoAsync()
        {
            var result = await shipperService.GetInfoAsync();
            return Ok(result);
        }

        [EnumAuthorize(Role.Administrator)]
        [HttpGet("page")]
        public async Task<IActionResult> GetShipperPagesAsync(int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            if (pageSize <= 0) return BadRequest(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            var result = await shipperService.GetShipperPageAsync(pageIndex, pageSize);
            return Ok(result);
        }
    }
}
