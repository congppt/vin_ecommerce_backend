using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.Base;
using VinEcomViewModel.Customer;
using VinEcomDomain.Resources;
using VinEcomRepository;
using Microsoft.AspNetCore.Authorization;
using VinEcomDomain.Enum;
using VinEcomAPI.CustomWebAttribute;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]s")]
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
            if (result is null) return Unauthorized(new { message = VinEcom.VINECOM_USER_AUTHORIZE_FAILED });
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CustomerSignUpViewModel vm)
        {
            var validateResult = await customerService.ValidateRegistrationAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            } 
            if (await customerService.IsPhoneExistAsync(vm.Phone)) return Conflict(new { message = VinEcom.VINECOM_USER_REGISTER_PHONE_DUPLICATED });
            if (!await customerService.IsBuildingExistedAsync(vm.BuildingId)) return Conflict(new { message = VinEcom.VINECOM_BUILDING_NOT_EXIST});
            var result = await customerService.RegisterAsync(vm);
            if (result) return Created("", new { message = VinEcom.VINECOM_USER_REGISTER_SUCCESS });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }
        //[EnumAuthorize(Role.Administrator)]
        [HttpGet("{id?}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await customerService.GetCustomerByIdAsync(id);
            if (result is not null) return Ok(result);
            return NotFound();
        }
        //[EnumAuthorize(Role.Administrator)]
        [HttpGet("page")]
        public async Task<IActionResult> GetCustomerPages(int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest(new { message = VinEcom.VINECOM_PAGE_INDEX_ERROR });
            if (pageSize <= 0) return BadRequest(new { message = VinEcom.VINECOM_PAGE_SIZE_ERROR });
            var result = await customerService.GetCustomerPagesAsync(pageIndex, pageSize);
            return Ok(result);
        }
        [EnumAuthorize(Role.Customer)]
        [HttpGet("info")]
        public async Task<IActionResult> GetPersonalInfo()
        {
            var result = await customerService.GetPersonalInfoAsync();
            return Ok(result);
        }
        [EnumAuthorize(Role.Customer)]
        [HttpPatch("info")]
        public async Task<IActionResult> UpdatePersonalBasicInfoAsync([FromBody] CustomerUpdateBasicViewModel vm)
        {
            var validateResult = await customerService.ValidateUpdateBasicAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            var isValidBuilding = await customerService.IsBuildingExistedAsync(vm.BuildingId);
            if (!isValidBuilding) return BadRequest(new { message = VinEcom.VINECOM_BUILDING_NOT_EXIST });
            var result = await customerService.UpdateBasicInfoAsync(vm);
            if (result) return Ok(new { message = VinEcom.VINECOM_UPDATE_SUCCESS});
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }
        [EnumAuthorize(Role.Customer)]
        [HttpPatch("password")]
        public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordViewModel vm)
        {
            var validateResult = await customerService.ValidateUpdatePasswordAsync(vm); 
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            if (!await customerService.IsCorrectCurrentPasswordAsync(vm)) return Conflict(new { message = VinEcom.VINECOM_CURRENT_PASSWORD_INCORRECT });
            var result = await customerService.UpdatePasswordAsync(vm);
            if (result) return Ok(new { message = VinEcom.VINECOM_UPDATE_SUCCESS });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }
        //[EnumAuthorize(Role.Administrator)]
        [HttpDelete("block")]
        public async Task<IActionResult> UpdateBlockStatusAsync([FromQuery] int customerId)
        {
            if (customerId <= 0) return BadRequest();
            //
            var customer = await customerService.FindCustomerAsync(customerId);
            if (customer is null) return NotFound(new { Message = VinEcom.VINECOM_CUSTOMER_NOT_FOUND });
            //
            var result = await customerService.UpdateBlockStatusAsync(customer);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_CUSTOMER_BLOCK_ERROR });
        }
    }
}
