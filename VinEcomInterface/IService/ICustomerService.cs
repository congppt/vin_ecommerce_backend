using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Account;

namespace VinEcomInterface.IService
{
    public interface ICustomerService
    {
        Task<CustomerAuthorizedViewModel?> AuthorizeAsync(string phone, string password);
    }
}
