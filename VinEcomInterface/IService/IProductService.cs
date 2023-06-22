using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Product;

namespace VinEcomInterface.IService
{
    public interface IProductService : IBaseService
    {
        Task<Pagination<Product>> GetProductFilterAsync(int pageIndex, int pageSize, ProductFilterModel filter);
        Task<Pagination<Product>> GetProductPagingAsync(int pageIndex, int pageSize);
        Task<IEnumerable<ProductRatingViewModel>> GetProductRatingAsync(List<int> productIds);
        Task<bool> AddAsync(ProductCreateModel product);
        Task<ValidationResult> ValidateCreateProductAsync(ProductCreateModel product);
        Task<ValidationResult> ValidateStoreProductFilterAsync(StoreProductFilterViewModel vm);
        Task<Pagination<Product>> GetStoreProductPageAsync(StoreProductFilterViewModel vm);
    }
}
