using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;

namespace VinEcomInterface.IRepository
{
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        Task<OrderDetail?> GetDetailByIdAndCustomerIdAsync(int id, int customerId);
        Task<IEnumerable<OrderDetail>> GetDetailsByProductIdAsync(int productId);
        Task<OrderDetail?> GetDetailByIdAsync(int id);
        Task<IEnumerable<OrderDetail>> GetDetailsByStoreIdAndStatusAsync(int storeId, OrderStatus status);
    }
}
