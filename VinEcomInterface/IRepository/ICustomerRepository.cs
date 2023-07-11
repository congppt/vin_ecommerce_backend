using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;

namespace VinEcomInterface.IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer?> AuthorizeAsync(string phone, string password);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> GetCustomerByUserIdAsync(int userId);
        Task<Pagination<Customer>> GetCustomerPagesAsync(int pageIndex, int pageSize);
    }
}
