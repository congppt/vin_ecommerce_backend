using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomDomain.Model;
using VinEcomInterface.IRepository;

namespace VinEcomRepository.Repository
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<OrderDetail?> GetDetailByIdAsync(int id)
        {
            return await context.Set<OrderDetail>()
                .AsNoTracking()
                .Include(x => x.Product)
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
    }
}
