using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;

namespace VinEcomInterface.IRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>?> GetOrderAtStateWithDetailsAsync(OrderStatus status, int? customerId);
        Task<Order?> GetCartByUserIdAndStoreId(int userId, int storeId);
        Task<Order?> GetCartByIdAsync(int id);
        Task<Order?> GetOrderWithDetailsAsync(int orderId, int? customerId);
        Task<Order?> GetStoreOrderWithDetailAsync(int orderId, int storeId);
        Task<Pagination<Order>> GetOrderPagesByStoreIdAndStatusAsync(int storeId, int status, int pageIndex, int pageSize);

    }
}
