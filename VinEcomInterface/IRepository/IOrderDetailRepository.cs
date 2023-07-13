using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;

namespace VinEcomInterface.IRepository
{
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        Task<IEnumerable<OrderDetail>> GetDetailsByProductIdAsync(int productId);
        Task<OrderDetail?> GetDetailByIdAsync(int id);
    }
}
