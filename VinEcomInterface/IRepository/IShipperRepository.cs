using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;

namespace VinEcomInterface.IRepository
{
    public interface IShipperRepository : IBaseRepository<Shipper>
    {
        Task<Shipper?> AuthorizeAsync(string phone, string password);
        Task<IEnumerable<Shipper>> GetAvailableShipperAsync();
        Task<Shipper?> GetShipperByUserId(int userId);
    }
}
