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
using VinEcomUtility.Pagination;
using VinEcomViewModel.Order;

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
                .AsNoTracking()
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
                                .AsNoTrackingWithIdentityResolution()
                                .Include(x => x.Details)
                                .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Store)
                                .ThenInclude(x => x.Building)
                                .Include(x => x.Customer)
                                .ThenInclude(x => x.User)
                                .Include(x => x.Building)
                                .Where(o => o.Status == status);
            if (customerId != null)
            {
                result = result.Where(o => o.CustomerId == customerId);
            }
            return await result.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await context.Set<Order>()
                .AsNoTracking()
                .Include(x => x.Details)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Store)
                .ThenInclude(x => x.Building)
                .Include(x => x.Customer)
                .ThenInclude(x => x.User)
                .Include(x => x.Building)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pagination<Order>> GetOrderPagesByCustomerIdAndStatusAsync(int customerId, int status, int pageIndex, int pageSize)
        {
            var sourse = context.Set<Order>()
                .Where(x => x.CustomerId == customerId && (int)x.Status == status);
            //
            var totalCount = await sourse.CountAsync();
            var items = await sourse
                .Include(x => x.Details)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Store)
                .AsNoTracking()
                .Skip(pageIndex * pageSize).Take(pageSize)
                .ToListAsync();
            //
            var result = new Pagination<Order>
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };
            return result;
        }

        public async Task<Pagination<Order>> GetOrderPagesByStoreIdAndStatusAsync(int storeId, int status, int pageIndex, int pageSize)
        {
            var sourse = context.Set<Order>()
                .Where(x => x.Details.Any(ord => ord.Product.StoreId == storeId) &&
                (int)x.Status == status);
            //
            var totalCount = await sourse.CountAsync();
            var items = await sourse
                .Include(x => x.Details)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Store)
                .AsNoTracking()
                .Skip(pageIndex * pageSize).Take(pageSize)
                .ToListAsync();
            //
            var result = new Pagination<Order>
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };
            return result;
        }

        public async Task<IEnumerable<Order>> GetOrdersByShipperIdAsync(int shipperId)
        {
            return await context.Set<Order>()
                .AsNoTracking()
                .Include(x => x.Details)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Store)
                .ThenInclude(x => x.Building)
                .Include(x => x.Customer)
                .ThenInclude(x => x.User)
                .Include(x => x.Building)
                .Where(x => x.ShipperId == shipperId)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId, int? customerId)
        {
            var result = await context.Set<Order>().AsNoTracking().Include(o => o.Details).FirstOrDefaultAsync(o => o.Id == orderId);
            if (result == null) return null;
            if (customerId.HasValue) return result.CustomerId == customerId ? result : null;
            return result;
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int numOfOrders)
        {
            var source = context.Set<Order>()
                .Include(x => x.Details)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Store)
                .AsNoTracking()
                .OrderByDescending(x => x.OrderDate)
                .Where(x => x.Status == OrderStatus.Done);
            //
            return await source
                .Take(numOfOrders)
                .ToListAsync();
        }

        public async Task<Order?> GetStoreOrderWithDetailAsync(int orderId, int storeId)
        {
            return await context.Set<Order>()
                .AsNoTracking()
                .Include(x => x.Details)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Store)
                .FirstOrDefaultAsync(x => x.Id == orderId &&
                x.Details.Any(ord => ord.Product.StoreId == storeId));
        }
    }
}
