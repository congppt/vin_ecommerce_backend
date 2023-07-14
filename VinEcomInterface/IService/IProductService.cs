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
        Task<Pagination<ProductViewModel>> GetProductFilterAsync(int pageIndex, int pageSize, ProductFilterModel filter);
        Task<bool> AddAsync(ProductCreateModel product);
        Task<ValidationResult> ValidateCreateProductAsync(ProductCreateModel product);
        Task<ValidationResult> ValidateFilterProductAsync(ProductFilterModel product);
        Task<ProductViewModel?> GetProductByIdAsync(int id, bool hideBlocked);
        Task<bool> RemoveAsync(int productId);
    }
}
