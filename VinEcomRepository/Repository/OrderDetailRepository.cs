using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomInterface.IRepository;

namespace VinEcomRepository.Repository
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<OrderDetail?> GetDetailByIdAndCustomerIdAsync(int id, int customerId)
        {
            return await context.Set<OrderDetail>()
                .FirstOrDefaultAsync(x => x.Id == id && x.Order.CustomerId == customerId);
        }

        public async Task<OrderDetail?> GetDetailByIdAsync(int id)
        {
            return await context.Set<OrderDetail>()
                .AsNoTracking()
                .Include(x => x.Product)
                .ThenInclude(x => x.Store)
                .Include(x => x.Order)
                .ThenInclude(x => x.Customer)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<OrderDetail>> GetDetailsByProductIdAsync(int productId)
        {
            return await context.Set<OrderDetail>()
                .AsNoTracking()
                .Include(x => x.Product)
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetDetailsByStoreIdAndStatusAsync(int storeId, OrderStatus status)
        {
            return await context.Set<OrderDetail>()
                .AsNoTracking()
                .Include(x => x.Product)
                .ThenInclude(x => x.Store)
                .Include(x => x.Order)
                .ThenInclude(x => x.Customer)
                .ThenInclude(x => x.User)
                .Where(x => x.Product.StoreId == storeId &&
                x.Order.Status == status)
                .ToListAsync();
        }
    }
}
