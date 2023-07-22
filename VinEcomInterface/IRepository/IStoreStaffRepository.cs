using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;

namespace VinEcomInterface.IRepository
{
    public interface IStoreStaffRepository : IBaseRepository<StoreStaff>
    {
        Task<StoreStaff?> AuthorizeAsync(string phone, string password);
        Task<StoreStaff?> GetStaffByIdAsync(int id);
        Task<Pagination<StoreStaff>> GetStaffPageAsync(int pageIndex, int pageSize);
    }
}
