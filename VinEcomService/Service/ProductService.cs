using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Product;

namespace VinEcomService.Service
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IConfiguration config, ITimeService timeService, ICacheService cacheService, IClaimService claimService) : base(unitOfWork, config, timeService, cacheService, claimService)
        {
        }

        #region GetProductPage
        public async Task<Pagination<Product>> GetProductPageAsync(int pageIndex = 0, int pageSize = 10)
        {
            return await unitOfWork.ProductRepository.GetPageAsync(pageIndex, pageSize);
        }

        #endregion

        #region FilterProduct
        public async Task<Pagination<Product>> GetProductFilterAsync(int pageIndex, int pageSize, ProductFilterModel filter)
        {
            if (!IsValidCategory(filter.Category))
                return new Pagination<Product>
                {
                    Items = new List<Product>(),
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                };
            return await unitOfWork.ProductRepository.GetProductFiltetAsync(pageIndex, pageSize, filter);
        }
        #endregion

        #region GetCategoryList
        public List<string> GetCategoryList()
        {
            return Enum.GetNames(typeof(ProductCategory)).ToList();
        }
        #endregion

        #region CheckValidCategory
        public bool IsValidCategory(int category)
        {
            return Enum.IsDefined(typeof(ProductCategory), category);
        }
        #endregion
    }
}
