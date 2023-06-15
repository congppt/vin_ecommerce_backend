using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Product;

namespace VinEcomInterface.IRepository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Pagination<Product>> GetProductFiltetAsync(int pageIndex, int pageSize, ProductFilterModel filter);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Pagination<Product>> GetPageAsync(int storeId, int pageIndex, int pageSize);
    }
}
