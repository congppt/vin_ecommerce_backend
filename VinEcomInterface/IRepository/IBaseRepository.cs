using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomUtility.Pagination;

namespace VinEcomInterface.IRepository
{
    public interface IBaseRepository<TModel>
    {
        Task<TModel?> GetByIdAsync(int id);
        Task AddAsync(TModel entity);
        void Update(TModel entity);
        void Delete(TModel entity);
        Task<Pagination<TModel>> GetPageAsync(int pageIndex, int pageSize);
        Task<IEnumerable<TModel>> GetAllAsync();
    }
}
