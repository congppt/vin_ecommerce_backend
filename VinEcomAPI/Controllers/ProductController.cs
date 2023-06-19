﻿using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using VinEcomDomain.Model;
using VinEcomDomain.Resources;
using VinEcomInterface.IService;
using VinEcomViewModel.Product;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        #region GetProductPage
        [HttpGet("Products")]
        public async Task<IActionResult> GetProductPagesAsync(int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest();
            if (pageSize <= 0) return BadRequest();
            var result = await productService.GetProductPageAsync(pageIndex, pageSize);
            return Ok(result);
        }
        #endregion

        #region GetProductPageByStoreId
        [HttpGet("ProductsByStoreId/{storeId?}")]
        public async Task<IActionResult> GetProductPagesByStoreIdAsync(int storeId, int pageIndex = 0, int pageSize = 10)
        {
            if (storeId < 0) return BadRequest();
            if (pageIndex < 0) return BadRequest();
            if (pageSize <= 0) return BadRequest();
            var result = await productService.GetProductPageByStoreIdAsync(storeId, pageIndex, pageSize);
            return Ok(result);
        }
        #endregion

        #region GetProductFilter
        [HttpPost("Filter")]
        public async Task<IActionResult> GetProductFilterAsync(ProductFilterModel filter, int pageIndex = 0, int pageSize = 10)
        {
            if (pageIndex < 0) return BadRequest();
            if (pageSize <= 0) return BadRequest();
            var products = await productService.GetProductFilterAsync(pageIndex, pageSize, filter);
            return Ok(products);
        }
        #endregion

        #region AddAsync
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddProductAsync(ProductCreateModel product)
        {
            var validateResult = await productService.ValidateCreateProductAsync(product);
            if (!await productService.IsExistsStoreAsync(product.StoreId)) validateResult.Errors
                    .Add(new ValidationFailure("StoreId", VinEcom.VINECOM_STORE_NOT_EXIST, product.StoreId));
            //
            if (!validateResult.IsValid) return BadRequest(validateResult.Errors);
            //
            if (await productService.AddAsync(product)) return Ok(product);
            return BadRequest();
        }
        #endregion
    }
}
