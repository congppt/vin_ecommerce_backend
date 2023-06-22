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

        public async Task<IEnumerable<OrderDetail>> GetDetailsByProductIdAsync(int productId)
        {
            return await context.Set<OrderDetail>()
                .Include(x => x.Product)
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }
    }
}
