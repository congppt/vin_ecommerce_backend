using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinEcomInterface.IService;
using VinEcomViewModel.OrderDetail;
using VinEcomDomain.Resources;
using VinEcomDomain.Enum;
using VinEcomRepository;

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
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartViewModel vm)
        {
            var result = await orderService.AddToCartAsync(vm);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, VinEcom.VINECOM_ORDER_ADDTOCART_FAILED);
        }

        #region RemoveFromCart
        [HttpPatch("RemoveFromCart/{productId?}")]
        public async Task<IActionResult> RemoveFromCartAsync(int productId)
        {
            if (productId <= 0) return BadRequest();
            var result = await orderService.RemoveFromCartAsync(productId); 
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_ORDER_REMOVE_FROM_CART_FAILED });
        }
        #endregion

        #region GetOrders
        [HttpGet("Orders")]
        public async Task<IActionResult> GetOrdersAsync(int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            if (pageSize <= 0) return BadRequest(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            var result = await orderService.GetOrdersAsync(pageIndex, pageSize);
            return Ok(result);
        }
        #endregion

        #region IsProductSameStore
        [HttpGet("IsProductSameStore/{productId?}")]
        public async Task<IActionResult> IsProductSameStoreAsync(int productId)
        {
            var result = await orderService.IsProductSameStoreAsync(productId);
            return Ok(result);
        }
        #endregion

        #region EmptyCart
        [HttpPut("EmptyCart")]
        public async Task<IActionResult> EmptyCartAsync(int id)
        {
            var result = await orderService.EmptyCartAsync(id);
            if (result) return Ok();
            return BadRequest();
        }
        #endregion

        #region StoreOrderPagesAtStatus
        [HttpGet("StoreOrderPagesByStatus")]
        public async Task<IActionResult> GetStoreOrderPagesByStatus(OrderStatus status, int pageIndex = 0, int pageSize = 10)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), status)) return BadRequest();
            if (pageIndex < 0) return BadRequest(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            if (pageSize <= 0) return BadRequest(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            var result = await orderService.GetStoreOrderPagesByStatus((int) status, pageIndex, pageSize);
            if (result is not null) return Ok(result);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_STORE_NOT_EXIST });
        }
        #endregion

        #region CustomerOrderPagesAtStatus
        [HttpGet("CustomerOrderPagesByStatus")]
        public async Task<IActionResult> GetCustomerOrderPagesByStatus(OrderStatus status, int pageIndex = 0, int pageSize = 10)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), status)) return BadRequest();
            if (pageIndex < 0) return BadRequest(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            if (pageSize <= 0) return BadRequest(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            var result = await orderService.GetCustomerOrderPagesByStatus((int) status, pageIndex, pageSize);
            if (result is not null) return Ok(result);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_CUSTOMER_NOT_FOUND });
        }
        #endregion

        #region GetCustomerOrder
        [HttpGet("CustomerOrders/{orderId?}")]
        public async Task<IActionResult> GetCustomerOrder(int orderId)
        {
            if (orderId <= 0) return BadRequest();
            var result = await orderService.GetCustomerOrdersAsync(orderId);
            if (result is null) return NotFound();
            return Ok(result);
        }
        #endregion

        #region GetStoreOrder
        [HttpGet("StoreOrders/{orderId}")]
        public async Task<IActionResult> GetStoreOrder(int orderId)
        {
            if (orderId <= 0) return BadRequest();
            var result = await orderService.GetStoreOrderAsync(orderId);
            if (result is null) return NotFound();
            return Ok(result);
        }
        #endregion

        #region Checkout
        [HttpPatch("Checkout")]
        public async Task<IActionResult> CheckoutAsync()
        {
            var result = await orderService.CheckoutAsync();
            if (result) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_ORDER_CHECKOUT_FAILED });
        }
        #endregion

        #region GetById
        [HttpGet("Orders/{id?}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await orderService.GetOrderByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        #endregion

        #region GetPendingOrder
        [HttpGet("PendingOrders")]
        public async Task<IActionResult> GetPendingOrdersAsync()
        {
            var result = await orderService.GetPendingOrdersAsync();
            return Ok(result);
        }
        #endregion
    }
}
