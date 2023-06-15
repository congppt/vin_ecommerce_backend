using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;

namespace VinEcomInterface.IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer?> AuthorizeAsync(string phone, string password);
    }
}
