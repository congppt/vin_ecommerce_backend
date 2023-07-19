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
using System.Numerics;
using VinEcomViewModel.Order;
using VinEcomDbContext.Migrations;
using System.Runtime.InteropServices;
using FluentValidation.Results;
using VinEcomInterface.IValidator;

namespace VinEcomService.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderValidator orderValidator;

        public OrderService(IUnitOfWork unitOfWork,
            IConfiguration config, ITimeService timeService,
            ICacheService cacheService, IClaimService claimService,
            IMapper mapper, IOrderValidator orderValidator) :
            base(unitOfWork, config, timeService, cacheService, claimService, mapper)
        {
            this.orderValidator = orderValidator;
        }

        #region AddToCart
        public async Task<bool> AddToCartAsync(AddToCartViewModel vm)
        {
            var customer = await FindCustomerAsync();
            if (customer is null) return false;
            var cart = (await unitOfWork.OrderRepository
                .GetOrderAtStateWithDetailsAsync(OrderStatus.Cart, customer.Id))
                .FirstOrDefault();
            var product = await unitOfWork.ProductRepository.GetProductByIdNoTrackingAsync(vm.ProductId);
            if (product is null) return false;
            //cart empty
            if (cart is null)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = vm.ProductId,
                    Quantity = vm.Quantity,
                };
                var newOrder = new Order
                {
                    OrderDate = timeService.GetCurrentTime(),
                    Status = OrderStatus.Cart,
                    CustomerId = customer.Id,
                    Details = new List<OrderDetail> { orderDetail }
                };
                await unitOfWork.OrderRepository.AddAsync(newOrder);
                return await unitOfWork.SaveChangesAsync();
            }
            //cart already contain product
            else
            {
                var detail = cart.Details.FirstOrDefault(d => d.ProductId == vm.ProductId);
                if (detail != null)
                {
                    detail.Quantity += vm.Quantity;
                    unitOfWork.OrderDetailRepository.Update(detail);
                    return await unitOfWork.SaveChangesAsync();
                }
                else
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = cart.Id,
                        ProductId = vm.ProductId,
                        Quantity = vm.Quantity,
                    };
                    await unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
                    return await unitOfWork.SaveChangesAsync();
                }
            }
        }
        #endregion

        #region ValidateAddToCart
        public async Task<ValidationResult> ValidateAddToCart(AddToCartViewModel vm)
        {
            return await orderValidator.CartAddValidator.ValidateAsync(vm);
        }
        #endregion

        #region RemoveFromCart
        public async Task<bool> RemoveFromCartAsync(int productId)
        {
            var customer = await FindCustomerAsync();
            if (customer == null) return false;
            var cart = (await unitOfWork.OrderRepository.GetOrderAtStateWithDetailsAsync(OrderStatus.Cart, customer.Id))!.FirstOrDefault();
            if (cart == null) return false;
            var detail = cart.Details.FirstOrDefault(x => x.ProductId == productId);
            if (detail != null)
            {
                unitOfWork.OrderDetailRepository.Delete(detail);
                if (cart.Details.Count == 0)
                {
                    unitOfWork.OrderRepository.Delete(cart);
                }
                return await unitOfWork.SaveChangesAsync();
            }

            return false;
        }
        #endregion

        #region EmptyCart
        public async Task<bool> EmptyCartAsync()
        {
            var customerId = claimService.GetRoleId();
            var cart = (await unitOfWork.OrderRepository
                .GetOrderAtStateWithDetailsAsync(OrderStatus.Cart, customerId))
                .FirstOrDefault();
            if (cart is not null)
            {
                unitOfWork.OrderRepository.Delete(cart);
                return await unitOfWork.SaveChangesAsync();
            }
            return false;
        }
        #endregion

        #region GetOrders
        public async Task<Pagination<OrderWithDetailsViewModel>> GetOrdersAsync(int pageIndex, int pageSize)
        {
            var orders = await unitOfWork.OrderRepository.GetOrderPageAsync(pageIndex, pageSize);
            return mapper.Map<Pagination<OrderWithDetailsViewModel>>(orders);
        }
        #endregion

        #region IsProductSameStore
        /// <summary>
        /// Return true if the added product's store is same as product's store in cart, otherwise false.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<bool> IsProductSameStoreAsync(int productId)
        {
            var product = await unitOfWork.ProductRepository.GetProductByIdAsync(productId);
            var customerId = claimService.GetRoleId();
            var cart = (await unitOfWork.OrderRepository
                .GetOrderAtStateWithDetailsAsync(OrderStatus.Cart, customerId))
                .FirstOrDefault();
            if (cart is null) return true;
            //
            if (product is not null)
            {
                var order = await unitOfWork.OrderRepository.GetCartByCustomerIdAndStoreId(customerId, product.StoreId);
                return order is not null;
            }
            return false;
        }
        #endregion

        #region StoreOrderPagesAtStatus
        public async Task<Pagination<OrderStoreViewModel>?> GetStoreOrderPagesByStatus(int status, int pageIndex, int pageSize, bool isSortDesc)
        {
            var storeId = claimService.GetStoreId();
            if (storeId <= 0) return null;
            var orders = await unitOfWork.OrderRepository.GetOrderPagesByStoreIdAndStatusAsync(storeId, status, pageIndex, pageSize);
            if (isSortDesc is true) orders.Items = orders.Items.OrderByDescending(x => x.Id).ToList();
            return mapper.Map<Pagination<OrderStoreViewModel>>(orders);
        }
        #endregion

        #region CustomerOrderPagesAtStatus
        public async Task<Pagination<OrderWithDetailsViewModel>?> GetCustomerOrderPagesByStatus(int status, int pageIndex, int pageSize)
        {
            var customer = await FindCustomerAsync();
            if (customer == null) return null;
            var orders = await unitOfWork.OrderRepository.GetOrderPagesByCustomerIdAndStatusAsync(customer.Id, status, pageIndex, pageSize);
            return mapper.Map<Pagination<OrderWithDetailsViewModel>>(orders);
        }
        #endregion

        #region GetCustomerOrders
        public async Task<OrderWithDetailsViewModel?> GetCustomerOrdersAsync(int orderId)
        {
            var customerId = claimService.GetRoleId();
            var orders = await unitOfWork.OrderRepository.GetOrderWithDetailsAsync(orderId, customerId);
            return mapper.Map<OrderWithDetailsViewModel>(orders);
        }
        #endregion

        #region GetStoreOrder
        public async Task<OrderStoreViewModel?> GetStoreOrderAsync(int orderId)
        {
            var storeId = claimService.GetStoreId();
            if (storeId <= 0) return null;
            var order = await unitOfWork.OrderRepository.GetStoreOrderWithDetailAsync(orderId, storeId);
            return mapper.Map<OrderStoreViewModel>(order);
        }
        #endregion

        #region Checkout
        public async Task<bool> CheckoutAsync()
        {
            var customer = await FindCustomerAsync();
            if (customer is null) return false;
            //
            var cart = (await unitOfWork.OrderRepository
                .GetOrderAtStateWithDetailsAsync(OrderStatus.Cart, customer.Id))?.FirstOrDefault();
            if (cart == null) return false;
            //
            if (await IsValidCartProductAsync(cart.Details.ToList()))
            {
                cart = await UpdateCartAsync(cart, customer);
                //
                unitOfWork.OrderRepository.Update(cart);
                return await unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        private async Task<bool> IsValidCartProductAsync(List<OrderDetail> details)
        {
            foreach (var item in details)
            {
                var product = await unitOfWork.ProductRepository.GetProductByIdNoTrackingAsync(item.ProductId);
                if (product is null) return false;
            }

            return true;
        }

        private async Task<Order> UpdateCartAsync(Order? cart, Customer customer)
        {
            foreach (var item in cart.Details)
            {
                var product = await unitOfWork.ProductRepository.GetProductByIdNoTrackingAsync(item.ProductId);
                item.Price = product.DiscountPrice.HasValue ? product.DiscountPrice : product.OriginalPrice;
            }
            //
            cart.ShipFee = 5000;
            cart.Status = OrderStatus.Preparing;
            cart.BuildingId = customer.BuildingId;
            //
            return cart;
        }
        #endregion

        #region GetById
        public async Task<OrderWithDetailsViewModel?> GetOrderVMByIdAsync(int id)
        {
            var order = await unitOfWork.OrderRepository.GetOrderByIdAsync(id);
            return mapper.Map<OrderWithDetailsViewModel>(order);
        }
        #endregion

        #region PendingOrders
        public async Task<IEnumerable<OrderWithDetailsViewModel>> GetPendingOrdersAsync()
        {
            var orders = await unitOfWork.OrderRepository.GetOrderAtStateWithDetailsAsync(OrderStatus.Preparing, null);
            orders = orders.OrderByDescending(x => x.OrderDate);
            return mapper.Map<IEnumerable<OrderWithDetailsViewModel>>(orders);
        }
        #endregion

        #region RecentOrders
        public async Task<IEnumerable<OrderWithDetailsViewModel>> GetRecentOrdersAsync(int numOfOrders)
        {
            var result = await unitOfWork.OrderRepository.GetRecentOrdersAsync(numOfOrders);
            return mapper.Map<IEnumerable<OrderWithDetailsViewModel>>(result);
        }
        #endregion

        #region GetDetailById
        public async Task<OrderDetailViewModel?> GetOrderDetailByIdAsync(int id)
        {
            var result = await unitOfWork.OrderDetailRepository.GetDetailByIdAsync(id);
            return mapper.Map<OrderDetailViewModel>(result);
        }
        #endregion

        #region CancelOrder
        public async Task<bool> CancelOrderAsync(Order order)
        {
            if (order is null || order.Status != OrderStatus.Preparing) return false;
            //
            var customerId = claimService.GetRoleId();
            if (customerId == -1 || order.CustomerId != customerId) return false;
            //
            order.Status = OrderStatus.Cancel;
            unitOfWork.OrderRepository.Update(order);
            return await unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region ValidateRating
        public async Task<ValidationResult> ValidateOrderRatingAsync(OrderDetailRatingViewModel vm)
        {
            return await orderValidator.OrderDetailRatingValidator.ValidateAsync(vm);
        }
        #endregion

        #region RatingOrder
        public async Task<bool> RatingOrderAsync(OrderDetailRatingViewModel vm)
        {
            int customerId = claimService.GetRoleId();
            var detail = await unitOfWork.OrderDetailRepository.GetDetailByIdAndCustomerIdAsync(vm.Id, customerId);
            if (detail is not null)
            {
                mapper.Map(vm, detail);
                unitOfWork.OrderDetailRepository.Update(detail);
                return await unitOfWork.SaveChangesAsync();
            }
            return false;
        }
        #endregion

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await unitOfWork.OrderRepository.GetByIdAsync(id);
        }

        private async Task<Customer?> FindCustomerAsync()
        {
            var userId = claimService.GetCurrentUserId();
            return await unitOfWork.CustomerRepository.GetCustomerByUserIdAsync(userId);
        }

        #region Total
        public async Task<decimal> GetOrderTotalAsync()
        {
            var total = 0m;
            var orders = await unitOfWork.OrderRepository.GetOrdersAsync();
            //
            foreach (var order in orders)
            {
                foreach (var detail in order.Details)
                {
                    total += detail.Price.HasValue ? detail.Price.Value : 0 * detail.Quantity;
                }
            }
            //
            return total;
        }
        #endregion
    }
}
