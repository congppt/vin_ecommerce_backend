using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomAPI.CustomWebAttribute;
using VinEcomDomain.Resources;
using VinEcomInterface.IService;
using VinEcomViewModel.Store;
using VinEcomDomain.Enum;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService storeService;
        public StoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterStoreAsync([FromBody] StoreRegisterViewModel vm)
        {
            var validationResult = await storeService.ValidateStoreRegistrationAsync(vm);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            if (!await storeService.IsBuildingExistedAsync(vm.BuildingId)) return Conflict(new { message = VinEcom.VINECOM_BUILDING_NOT_EXIST });
            if (await storeService.RegisterAsync(vm)) return Ok(new { message = VinEcom.VINECOM_STORE_REGISTER_SUCCESS });
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = VinEcom.VINECOM_SERVER_ERROR });
        }
        [HttpPost("filter")]
        public async Task<IActionResult> GetStoresByFilterAsync([FromBody] StoreFilterViewModel vm)
        {
            var validateResult = await storeService.ValidateStoreFilterAsync(vm);
            if (!validateResult.IsValid)
            {
                var errors = validateResult.Errors.Select(e => new { property = e.PropertyName, message = e.ErrorMessage });
                return BadRequest(errors);
            }
            var result = await storeService.GetStoreFilterResultAsync(vm);
            return Ok(result);
        }

        [HttpDelete("block")]
        public async Task<IActionResult> UpdateBlockStatus(int storeId)
        {
            if (storeId < 0) return BadRequest();
            var store = await storeService.FindStoreAsync(storeId);
            if (store == null) return NotFound(new { message = VinEcom.VINECOM_STORE_NOT_EXIST });
            var result = await storeService.ChangeBlockStatusAsync(store);
            if (result) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_STORE_CHANGE_STATUS_ERROR });
        }

        #region UpdateWorkingStatus
        [EnumAuthorize(Role.Staff)]
        [HttpPut("working-status")]
        public async Task<IActionResult> UpdateWorkingStatusAsync()
        {
            var result = await storeService.UpdateWorkingStatus();
            if (result) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_STORE_CHANGE_STATUS_ERROR });
        }
        #endregion

        #region GetAll
        [EnumAuthorize(Role.Administrator | Role.Customer)]
        [HttpGet("page")]
        public async Task<IActionResult> GetStorePagesAsync(int pageIndex = 0, int pageSize = 10, bool isSortDesc = false)
        {
            if (pageIndex < 0) return BadRequest(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            if (pageSize <= 0) return BadRequest(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            var result = await storeService.GetStorePagesAsync(pageIndex, pageSize, isSortDesc);
            return Ok(result);
        }
        #endregion

        #region GetById
        [HttpGet("{id?}")]
        public async Task<IActionResult> GetByIdAsync(int id, bool isBlocked = false)
        {
            if (id <= 0) return BadRequest();
            var result = await storeService.GetByIdAsync(id, isBlocked);
            if (result is not null) return Ok(result);
            return NotFound(new { Message = VinEcom.VINECOM_STORE_NOT_EXIST });
        }
        #endregion

        #region GetReviews
        [EnumAuthorize(Role.Staff)]
        [HttpGet("reviews")]
        public async Task<IActionResult> GetStoreReviewsAsync(bool isSortDesc = false)
        {
            var result = await storeService.GetStoreReviewAsync(isSortDesc);
            return Ok(result);
        }
        #endregion

        #region GetStoreTotalOrder
        [EnumAuthorize(Role.Staff)]
        [HttpGet("order/total")]
        public async Task<IActionResult> GetStoreOrderTotal(OrderStatus? status = null)
        {
            if (status.HasValue && status.Value == OrderStatus.Cart) return BadRequest();
            var result = await storeService.GetStoreOrderTotalAsync(status);
            return Ok(new { total = result });
        }
        #endregion
    }
}
