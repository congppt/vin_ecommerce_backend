using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomDomain.Model;
using VinEcomInterface.IRepository;
using VinEcomUtility.Pagination;
using VinEcomUtility.UtilityMethod;

namespace VinEcomRepository.Repository
{
    public class ShipperRepository : BaseRepository<Shipper>, IShipperRepository
    {
        public ShipperRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Pagination<Shipper>> GetShipperPageAsync(int pageIndex, int pageSize)
        {
            var totalCount = await context.Set<Shipper>().CountAsync();
            var items = await context.Set<Shipper>()
                .AsNoTracking()
                .Skip(pageIndex * pageSize).Take(pageSize)
                .Include(x => x.User)
                .ToListAsync();
            var result = new Pagination<Shipper>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };
            //
            return result;
        }

        public async Task<Shipper?> AuthorizeAsync(string phone, string password)
        {
            var shipper = await context.Set<Shipper>()
                                .AsNoTracking()
                                .Include(c => c.User)
                                .FirstOrDefaultAsync(c => c.User.Phone == phone && !c.User.IsBlocked);
            if (shipper == null) return null;
            if (password.IsCorrectHashSource(shipper.User.PasswordHash)) return shipper;
            return null;
        }

        public async Task<IEnumerable<Shipper>> GetAvailableShipperAsync()
        {
            return await context.Set<Shipper>()
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.Status == VinEcomDomain.Enum.ShipperStatus.Available)
                .ToListAsync();
        }

        public async Task<Shipper?> GetShipperByIdAsync(int shipperId)
        {
            return await context.Set<Shipper>()
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == shipperId);
        }

        public async Task<Shipper?> GetShipperByUserId(int userId)
        {
            return await context.Set<Shipper>()
                .Include(x => x.User)
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
