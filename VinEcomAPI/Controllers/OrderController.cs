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

        #region AddToCart
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartViewModel vm)
        {
            var result = await orderService.AddToCartAsync(vm);
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, VinEcom.VINECOM_ORDER_ADDTOCART_FAILED);
        }
        #endregion

        #region RemoveFromCart
        [HttpPatch("remove-from-cart/{productId?}")]
        public async Task<IActionResult> RemoveFromCartAsync(int productId)
        {
            if (productId <= 0) return BadRequest();
            var result = await orderService.RemoveFromCartAsync(productId); 
            if (result is true) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_ORDER_REMOVE_FROM_CART_FAILED });
        }
        #endregion

        #region GetOrders
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrdersAsync(int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            if (pageSize <= 0) return BadRequest(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            var result = await orderService.GetOrdersAsync(pageIndex, pageSize);
            return Ok(result);
        }
        #endregion

        #region IsProductSameStore
        [HttpGet("is-product-same-store/{productId?}")]
        public async Task<IActionResult> IsProductSameStoreAsync(int productId)
        {
            var result = await orderService.IsProductSameStoreAsync(productId);
            return Ok(result);
        }
        #endregion

        #region EmptyCart
        [HttpPut("empty-cart")]
        public async Task<IActionResult> EmptyCartAsync(int id)
        {
            var result = await orderService.EmptyCartAsync(id);
            if (result) return Ok();
            return BadRequest();
        }
        #endregion

        #region StoreOrderPagesAtStatus
        [HttpGet("store-order-pages-by-status")]
        public async Task<IActionResult> GetStoreOrderPagesByStatus(OrderStatus status, int pageIndex = 0, int pageSize = 10)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), status)) return BadRequest();
            if (pageIndex < 0) return BadRequest(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            if (pageSize <= 0) return BadRequest(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            var result = await orderService.GetStoreOrderPagesByStatus((int) status, pageIndex, pageSize);
            if (result is not null) return Ok(result);
            return StatusCode(StatusCodes.Status400BadRequest, new { Message = VinEcom.VINECOM_STORE_NOT_EXIST });
        }
        #endregion

        #region CustomerOrderPagesAtStatus
        [HttpGet("customer-order-pages-by-status")]
        public async Task<IActionResult> GetCustomerOrderPagesByStatus(OrderStatus status, int pageIndex = 0, int pageSize = 10)
        {
            if (!Enum.IsDefined(typeof(OrderStatus), status)) return BadRequest();
            if (pageIndex < 0) return BadRequest(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            if (pageSize <= 0) return BadRequest(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            var result = await orderService.GetCustomerOrderPagesByStatus((int) status, pageIndex, pageSize);
            if (result is not null) return Ok(result);
            return StatusCode(StatusCodes.Status400BadRequest, new { Message = VinEcom.VINECOM_CUSTOMER_NOT_FOUND });
        }
        #endregion

        #region GetCustomerOrder
        [HttpGet("customer-orders/{orderId?}")]
        public async Task<IActionResult> GetCustomerOrder(int orderId)
        {
            if (orderId <= 0) return BadRequest();
            var result = await orderService.GetCustomerOrdersAsync(orderId);
            if (result is null) return NotFound();
            return Ok(result);
        }
        #endregion

        #region GetStoreOrder
        [HttpGet("store-orders/{orderId}")]
        public async Task<IActionResult> GetStoreOrder(int orderId)
        {
            if (orderId <= 0) return BadRequest();
            var result = await orderService.GetStoreOrderAsync(orderId);
            if (result is null) return NotFound();
            return Ok(result);
        }
        #endregion

        #region Checkout
        [HttpPatch("checkout")]
        public async Task<IActionResult> CheckoutAsync()
        {
            var result = await orderService.CheckoutAsync();
            if (result) return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_ORDER_CHECKOUT_FAILED });
        }
        #endregion

        #region GetById
        [HttpGet("orders/{id?}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await orderService.GetOrderByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        #endregion

        #region GetPendingOrder
        [HttpGet("pending-orders")]
        public async Task<IActionResult> GetPendingOrdersAsync()
        {
            var result = await orderService.GetPendingOrdersAsync();
            return Ok(result);
        }
        #endregion

        #region GetRecentOrders
        [HttpGet("recent-orders/{numOfOrders?}")]
        public async Task<IActionResult> GetRecentOrders(int numOfOrders = 10)
        {
            if (numOfOrders <= 0) return BadRequest(new { Message = VinEcom.VINECOM_NUM_OF_ORDERS_ERROR });
            var result = await orderService.GetRecentOrdersAsync(numOfOrders);
            return Ok(result);
        }
        #endregion
    }
}
