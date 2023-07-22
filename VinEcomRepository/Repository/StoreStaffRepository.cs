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
    public class StoreStaffRepository : BaseRepository<StoreStaff>, IStoreStaffRepository
    {
        public StoreStaffRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<StoreStaff?> AuthorizeAsync(string phone, string password)
        {
            var staff = await context.Set<StoreStaff>()
                                .AsNoTracking()
                                .Include(c => c.User)
                                .FirstOrDefaultAsync(c => c.User.Phone == phone && !c.User.IsBlocked);
            if (staff == null) return null;
            if (password.IsCorrectHashSource(staff.User.PasswordHash)) return staff;
            return null;
        }

        public async Task<StoreStaff?> GetStaffByIdAsync(int id)
        {
            return await context.Set<StoreStaff>()
                .AsNoTracking()
                .Include(x => x.Store)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Pagination<StoreStaff>> GetStaffPageAsync(int pageIndex, int pageSize)
        {
            var totalCount = await context.Set<StoreStaff>().CountAsync();
            var items = await context.Set<StoreStaff>()
                .AsNoTracking()
                .Include(x => x.Store)
                .Include(x => x.User)
                .Skip(pageIndex * pageSize).Take(pageSize)
                .ToListAsync();
            var result = new Pagination<StoreStaff>
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };
            //
            return result;
        }
    }
}
