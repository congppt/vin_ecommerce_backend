using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Order;

namespace VinEcomInterface.IRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Pagination<Order>> GetOrderPageAsync(int pageIndex, int pageSize);
        Task<IEnumerable<Order>?> GetOrderAtStateWithDetailsAsync(OrderStatus status, int? customerId);
        Task<Order?> GetCartByCustomerIdAndStoreId(int customerId, int storeId);
        Task<Order?> GetCartByIdAsync(int id);
        Task<Order?> GetOrderWithDetailsAsync(int orderId, int? customerId);
        Task<Order?> GetStoreOrderWithDetailAsync(int orderId, int storeId);
        Task<Pagination<Order>> GetOrderPagesByStoreIdAndStatusAsync(int storeId, int status, int pageIndex, int pageSize);
        Task<Pagination<Order>> GetOrderPagesByCustomerIdAndStatusAsync(int customerId, int status, int pageIndex, int pageSize);
        Task<IEnumerable<Order>> GetOrdersByShipperIdAsync(int shipperId);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetRecentOrdersAsync(int numOfOrders);
        Task<IEnumerable<Order>> GetOrderByStoreIdAndStateAsync(int storeId, OrderStatus? status);
        Task<IEnumerable<Order>> GetOrdersAsync();
    }
}
