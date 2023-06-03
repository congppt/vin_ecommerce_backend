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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByPhone(string phone)
        {
            var user = await context.Set<User>()
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.Phone == phone);
            return user;
        }
    }
}
