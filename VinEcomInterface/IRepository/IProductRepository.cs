using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Product;

namespace VinEcomInterface.IRepository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Pagination<Product>> GetProductPagingAsync(int pageIndex, int pageSize);
        Task<Pagination<Product>> GetProductByCategoryAsync(ProductCategory category, int pageIndex, int pageSize);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductByIdNoTrackingAsync(int id);
        Task<Pagination<Product>> GetStoreProductPageAsync(StoreProductFilterViewModel vm);
        Task<Pagination<Product>> GetProductPagesByNameAsync(string name, int pageIndex, int pageSize);
        Task<Product?> GetProductByIdAsync(int id, bool hideBlocked);
    }
}
