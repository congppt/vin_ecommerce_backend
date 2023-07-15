using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomDomain.Model;
using VinEcomInterface.IRepository;
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
    }
}
