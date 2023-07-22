using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;

namespace VinEcomInterface.IRepository
{
    public interface IShipperRepository : IBaseRepository<Shipper>
    {
        Task<Pagination<Shipper>> GetShipperPageAsync(int pageIndex, int pageSize);
        Task<Shipper?> AuthorizeAsync(string phone, string password);
        Task<IEnumerable<Shipper>> GetAvailableShipperAsync();
        Task<Shipper?> GetShipperByUserId(int userId);
        Task<Shipper?> GetShipperByIdAsync(int shipperId);
    }
}
