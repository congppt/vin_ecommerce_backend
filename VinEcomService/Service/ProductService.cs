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

        #region Products
        public async Task<Pagination<ProductViewModel>> GetProductFilterAsync(int pageIndex, int pageSize, ProductFilterModel filter)
        {
            //Get Paging by Category
            if (filter.Category.HasValue)
            {
                var result = await unitOfWork.ProductRepository.GetProductByCategoryAsync(filter.Category.Value, pageIndex, pageSize);
                return mapper.Map<Pagination<ProductViewModel>>(result);
            }
            //Get paging by Store Id
            if (filter.StoreId.HasValue)
            {
                var result = await unitOfWork.ProductRepository.GetStoreProductPageAsync(new StoreProductFilterViewModel
                {
                    StoreId = filter.StoreId.Value,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });
                return mapper.Map<Pagination<ProductViewModel>>(result);
            }
            //Get paging by name
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var result = await unitOfWork.ProductRepository.GetProductPagesByNameAsync(filter.Name, pageIndex, pageSize);
                return mapper.Map<Pagination<ProductViewModel>>(result);
            }
            //Origin product paging
            var productPages = await unitOfWork.ProductRepository.GetProductPagingAsync(pageIndex, pageSize);
            return mapper.Map<Pagination<ProductViewModel>>(productPages);
        }
        #endregion

        #region Add
        public async Task<bool> AddAsync(ProductCreateModel product)
        {
            var storeId = claimService.GetStoreId();
            if (storeId < 0) return false;
            //
            var createProduct = mapper.Map<Product>(product);
            createProduct.StoreId = storeId;
            //
            await unitOfWork.ProductRepository.AddAsync(createProduct);
            return await unitOfWork.SaveChangesAsync();
        }
        #endregion

        public async Task<ValidationResult> ValidateCreateProductAsync(ProductCreateModel product)
        {
            return await productValidator.ProductCreateValidator.ValidateAsync(product);
        }

        public async Task<ValidationResult> ValidateFilterProductAsync(ProductFilterModel product)
        {
            return await productValidator.ProductFilterValidator.ValidateAsync(product);
        }

        public async Task<ProductViewModel?> GetProductByIdAsync(int id, bool hideBlocked)
        {
            var product = await unitOfWork.ProductRepository.GetProductByIdAsync(id, hideBlocked);
            var vm = mapper.Map<ProductViewModel>(product);
            return vm;
        }
        public async Task<bool> RemoveAsync(int productId)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(productId);
            product.IsRemoved = !product.IsRemoved;
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> SetOutOfStockAsync(int productId)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(productId);
            product.IsOutOfStock = !product.IsOutOfStock;
            return await unitOfWork.SaveChangesAsync();
        }
    }
}
