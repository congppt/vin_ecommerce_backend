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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Customer?> AuthorizeAsync(string phone, string password)
        {
            var customer = await context.Set<Customer>()
                                .AsNoTracking()
                                .Include(c => c.User)
                                .FirstOrDefaultAsync(c => c.User.Phone == phone);
            if (customer == null) return null;
            if (password.IsCorrectHashSource(customer.User.PasswordHash)) return customer;
            return null;
        }
    }
}
