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
                                .FirstOrDefaultAsync(c => c.User.Phone == phone && !c.User.IsBlocked);
            if (customer == null) return null;
            if (password.IsCorrectHashSource(customer.User.PasswordHash)) return customer;
            return null;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await context.Set<Customer>()
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Building)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer?> GetCustomerByUserIdAsync(int userId)
        {
            return await context.Set<Customer>()
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Building)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Pagination<Customer>> GetCustomerPagesAsync(int pageIndex, int pageSize)
        {
            var totalCount = await context.Set<Customer>().CountAsync();
            var items = await context.Set<Customer>()
                .AsNoTracking().Include(x => x.User)
                .Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var result = new Pagination<Customer>()
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
