using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
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
using VinEcomInterface.IValidator;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Product;

namespace VinEcomService.Service
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductValidator productValidator;
        public ProductService(IUnitOfWork unitOfWork,
                              IConfiguration config,
                              ITimeService timeService,
                              ICacheService cacheService,
                              IClaimService claimService,
                              IMapper mapper,
                              IProductValidator productValidator) : 
            base(unitOfWork, config, timeService,
                cacheService, claimService, mapper)
        {
            this.productValidator = productValidator;
        }
        public async Task<Pagination<Product>> GetProductPageAsync(int storeId, int pageIndex = 0, int pageSize = 10)
        {
            return await unitOfWork.ProductRepository.GetPageAsync(pageIndex, pageSize, storeId);
        }
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
        public List<string> GetCategoryList()
        {
            return Enum.GetNames(typeof(ProductCategory)).ToList();
        }
        public bool IsValidCategory(int category)
        {
            return Enum.IsDefined(typeof(ProductCategory), category);
        }
        public async Task<bool> AddAsync(ProductCreateModel product)
        {
            var result = await productValidator.ProductCreateValidator.ValidateAsync(product);
            if (result.IsValid)
            {
                var createProduct = mapper.Map<Product>(product);
                await unitOfWork.ProductRepository.AddAsync(createProduct);
                return await unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        public async Task<ValidationResult> ValidateStoreProductFilterAsync(StoreProductFilterViewModel vm)
        {
            return await productValidator.StoreProductFilterValidator.ValidateAsync(vm);
        }
        public async Task<Pagination<Product>> GetStoreProductPageAsync(StoreProductFilterViewModel vm)
        {
            return await unitOfWork.ProductRepository.GetStoreProductPageAsync(vm);
        }
    }
}
