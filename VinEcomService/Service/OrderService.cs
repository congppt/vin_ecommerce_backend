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

namespace VinEcomService.Service
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork, IConfiguration config, ITimeService timeService, ICacheService cacheService, IClaimService claimService) : base(unitOfWork, config, timeService, cacheService, claimService)
        {
        }

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

        #region GetOrders
        public async Task<Pagination<Order>> GetOrdersAsync(int pageIndex, int pageSize)
        {
            return await unitOfWork.OrderRepository.GetPageAsync(pageIndex, pageSize); 
        }
        #endregion
    }
}
