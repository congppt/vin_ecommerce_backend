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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> AuthorizeAsync(string phone, string password)
        {
            var admin = await context.Set<User>()
                                .AsNoTracking()
                                .FirstOrDefaultAsync(c => c.Phone == phone && !c.IsBlocked);
            if (admin == null) return null;
            if (password.IsCorrectHashSource(admin.PasswordHash)) return admin;
            return null;
        }

        public async Task<User?> GetByPhone(string phone)
        {
            var user = await context.Set<User>()
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.Phone == phone);
            return user;
        }

        public async Task<bool> IsPasswordCorrectAsync(int id, string password)
        {
            //disable tracking lower performance
            var user = await context.Set<User>().FirstOrDefaultAsync(u => u.Id == id);
            return password.IsCorrectHashSource(user.PasswordHash);
        }
    }
}
