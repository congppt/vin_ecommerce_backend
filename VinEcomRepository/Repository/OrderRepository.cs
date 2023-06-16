using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomDbContext.Migrations;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomInterface.IRepository;

namespace VinEcomRepository.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Order?> GetCartByIdAsync(int id)
        {
            return await context.Set<Order>()
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == id && x.Status == OrderStatus.Cart);
        }

        public async Task<Order?> GetCartByUserIdAndStoreId(int userId, int storeId)
        {
            return await context.Set<Order>()
                .AsNoTracking()
                .Include(x => x.Details)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Store)
                .FirstOrDefaultAsync(x => x.CustomerId == userId && x.Status == OrderStatus.Cart &&
                x.Details.Any(det => det.Product.Store.Id == storeId));
        }

        public async Task<IEnumerable<Order>?> GetOrderAtStateWithDetailsAsync(OrderStatus status, int? customerId)
        {
            var result = context.Set<Order>()
                                .AsNoTracking()
                                .Include(o => o.Details)
                                .ThenInclude(d => d.Product)
                                .Where(o => o.Status == status);
            if (customerId != null)
            {
                result = result.Where(o => o.CustomerId == customerId);
            }
            return await result.ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId, int? customerId)
        {
            var result = await context.Set<Order>().AsNoTracking().Include(o => o.Details).FirstOrDefaultAsync(o => o.Id == orderId);
            if (result == null) return null;
            if (customerId.HasValue) return result.CustomerId == customerId ? result : null;
            return result;
        }
    }
}
