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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Customer?> AuthorizeAsync(string phone, string passwordHash)
        {
            return await context.Set<Customer>()
                                .AsNoTracking()
                                .Include(c => c.User)
                                .FirstOrDefaultAsync(c => c.User.Phone == phone && c.User.PasswordHash == passwordHash);
        }
    }
}
