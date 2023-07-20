using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;

namespace VinEcomInterface.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> AuthorizeAsync(string phone, string password);
        Task<User?> GetByPhone(string phone);
        Task<bool> IsPasswordCorrectAsync(int id, string password);
    }
}
