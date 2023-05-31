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
    public class StoreStaffRepository : BaseRepository<StoreStaff>, IStoreStaffRepository
    {
        public StoreStaffRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<StoreStaff?> AuthorizeAsync(string phone, string passwordHash)
        {
            return await context.Set<StoreStaff>()
                                .AsNoTracking()
                                .Include(c => c.User)
                                .FirstOrDefaultAsync(c => c.User.Phone == phone && c.User.PasswordHash == passwordHash);
        }
    }
}
