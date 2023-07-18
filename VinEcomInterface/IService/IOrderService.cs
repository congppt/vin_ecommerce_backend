using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Order;
using VinEcomViewModel.OrderDetail;

namespace VinEcomInterface.IService
{
    public interface IOrderService : IBaseService
    {
        Task<bool> AddToCartAsync(AddToCartViewModel vm);
        Task<ValidationResult> ValidateAddToCart(AddToCartViewModel vm);
        Task<bool> RemoveFromCartAsync(int productId);
        Task<Pagination<OrderWithDetailsViewModel>> GetOrdersAsync(int pageIndex, int pageSize);
        Task<bool> IsProductSameStoreAsync(int productId);
        Task<bool> EmptyCartAsync();
        Task<Pagination<OrderStoreViewModel>?> GetStoreOrderPagesByStatus(int status, int pageIndex, int pageSize, bool isSortDesc);
        Task<Pagination<OrderWithDetailsViewModel>?> GetCustomerOrderPagesByStatus(int status, int pageIndex, int pageSize);
        Task<OrderWithDetailsViewModel?> GetCustomerOrdersAsync(int orderId);
        Task<OrderStoreViewModel?> GetStoreOrderAsync(int orderId);
        Task<bool> CheckoutAsync();
        Task<OrderWithDetailsViewModel?> GetOrderVMByIdAsync(int id);
        Task<IEnumerable<OrderWithDetailsViewModel>> GetPendingOrdersAsync();
        Task<IEnumerable<OrderWithDetailsViewModel>> GetRecentOrdersAsync(int numOfOrders);
        Task<OrderDetailViewModel?> GetOrderDetailByIdAsync(int id);
        Task<bool> CancelOrderAsync(Order order);
        Task<decimal> GetOrderTotalAsync();
        Task<bool> RatingOrderAsync(OrderDetailRatingViewModel vm);
        Task<ValidationResult> ValidateOrderRatingAsync(OrderDetailRatingViewModel vm);
        //
        Task<Order?> GetOrderByIdAsync (int id);
    }
}
