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
    public class ShipperRepository : BaseRepository<Shipper>, IShipperRepository
    {
        public ShipperRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Shipper?> AuthorizeAsync(string phone, string passwordHash)
        {
            return await context.Set<Shipper>()
                                .AsNoTracking()
                                .Include(c => c.User)
                                .FirstOrDefaultAsync(c => c.User.Phone == phone && c.User.PasswordHash == passwordHash);
        }
    }
}
