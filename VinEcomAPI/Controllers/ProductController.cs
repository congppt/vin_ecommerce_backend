using FluentValidation.Results;
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

        [HttpGet("Products")]
        public async Task<IActionResult> GetProductPage([FromQuery] ProductFilterModel filter, int pageIndex = 0, int pageSize = 10)
        {
            if (filter.Category.HasValue)
            {
                var validateResult = await productService.ValidateFilterProductAsync(filter);
                if (!validateResult.IsValid) return BadRequest(validateResult.Errors);
            }
            if (pageIndex < 0) return BadRequest(new { Message = VinEcom.VINECOM_PAGE_INDEX_ERROR });
            if (pageSize <= 0) return BadRequest(new { Message = VinEcom.VINECOM_PAGE_SIZE_ERROR });
            var products = await productService.GetProductFilterAsync(pageIndex, pageSize, filter);
            return Ok(products);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductCreateModel product)
        {
            var validateResult = await productService.ValidateCreateProductAsync(product);
            if (!validateResult.IsValid) return BadRequest(validateResult.Errors);
            //
            if (await productService.AddAsync(product)) return Ok(product);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_PRODUCT_CREATE_ERROR });
        }
    }
}
