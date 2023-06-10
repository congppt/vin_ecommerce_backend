using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.OrderDetail;
using VinEcomDomain.Resources;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet("AddToCart")]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartViewModel vm)
        {
            var result = await orderService.AddToCartAsync(vm);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, VinEcom.VINECOM_ORDER_ADDTOCART_FAILED);
        }

        #region GetOrders
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrdersAsync(int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest();
            if (pageSize <= 0) return BadRequest();
            var result = await orderService.GetOrdersAsync(pageIndex, pageSize);
            return Ok(result);
        }
        #endregion

        #region IsProductSameStore
        [HttpGet("IsProductSameStore")]
        public async Task<IActionResult> IsProductSameStoreAsync(int productId)
        {
            var result = await orderService.IsProductSameStoreAsync(productId);
            return Ok(result);
        }
        #endregion
    }
}
