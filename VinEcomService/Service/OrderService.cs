using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomDomain.Enum;
using VinEcomViewModel.OrderDetail;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using AutoMapper;

namespace VinEcomService.Service
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork,
                            IConfiguration config,
                            ITimeService timeService,
                            ICacheService cacheService,
                            IClaimService claimService,
                            IMapper mapper) : base(unitOfWork, config, timeService, cacheService, claimService, mapper)
        { }

        public async Task<bool> AddToCartAsync(AddToCartViewModel vm)
        {
            var customerId = claimService.GetCurrentUserId();
            var cart = (await unitOfWork.OrderRepository.GetOrderAtStateWithDetailsAsync(OrderStatus.Cart, customerId)).FirstOrDefault();
            //cart empty
            if (cart is null)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = vm.ProductId,
                    Quantity = vm.Quantity
                };
                var newOrder = new Order
                {
                    OrderDate = timeService.GetCurrentTime(),
                    Status = OrderStatus.Cart,
                    CustomerId = customerId,
                    Details = new List<OrderDetail> { orderDetail }
                };
                await unitOfWork.OrderRepository.AddAsync(newOrder);
                if (await unitOfWork.SaveChangesAsync()) return true;
                return false;
            }
            //cart already contain product
            else
            {
                var detail = cart.Details.FirstOrDefault(d => d.ProductId == vm.ProductId);
                if (detail != null)
                {
                    detail.Quantity += vm.Quantity;
                    unitOfWork.OrderDetailRepository.Update(detail);
                    if (await unitOfWork.SaveChangesAsync()) return true;
                    return false;
                }
                else
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = cart.Id,
                        ProductId = vm.ProductId,
                        Quantity = vm.Quantity
                    };
                    await unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
                    if (await unitOfWork.SaveChangesAsync()) return true;
                    return false;
                }
            }
        }

        public async Task<bool> EmptyCartAsync(int cartId)
        {
            var cart = await unitOfWork.OrderRepository.GetCartByIdAsync(cartId);
            if (cart is not null)
            {
                cart.Details = new List<OrderDetail>();
                unitOfWork.OrderRepository.Update(cart);
                return await unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        #region GetOrders
        public async Task<Pagination<Order>> GetOrdersAsync(int pageIndex, int pageSize)
        {
            return await unitOfWork.OrderRepository.GetPageAsync(pageIndex, pageSize); 
        }
        #endregion

        #region IsProductSameStore
        public async Task<bool> IsProductSameStoreAsync(int productId)
        {
            var product = await unitOfWork.ProductRepository.GetProductByIdAsync(productId);
            var userId = claimService.GetCurrentUserId();
            if (product is not null && userId != -1)
            {
                var order = await unitOfWork.OrderRepository.GetCartByUserIdAndStoreId(userId, product.StoreId);
                return order is not null;
            }
            return false;
        }
        #endregion

        #region StoreOrderPagesAtStatus
        public async Task<Pagination<Order>?> GetStoreOrderPagesByStatus(int status, int pageIndex, int pageSize)
        {
            var storeId = claimService.GetStoreId();
            if (storeId <= 0) return null; 
            return await unitOfWork.OrderRepository.GetOrderPagesByStoreIdAndStatusAsync(storeId, status, pageIndex, pageSize);
        }
        #endregion

        #region GetCustomerOrders
        public async Task<Order?> GetCustomerOrdersAsync(int orderId)
        {
            var userId = claimService.GetCurrentUserId();
            return await unitOfWork.OrderRepository.GetOrderWithDetailsAsync(orderId, userId);
        }
        #endregion

        #region GetStoreOrder
        public async Task<Order?> GetStoreOrderAsync(int orderId)
        {
            var storeId = claimService.GetStoreId();
            if (storeId <= 0) return null;
            return await unitOfWork.OrderRepository.GetStoreOrderWithDetailAsync(orderId, storeId);
        }
        #endregion
    }
}
