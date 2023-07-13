using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using VinEcomAPI.CustomWebAttribute;
using VinEcomDomain.Model;
using VinEcomDomain.Resources;
using VinEcomInterface.IService;
using VinEcomViewModel.Product;
using VinEcomDomain.Enum;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace VinEcomAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IClaimService claimService;
        public ProductController(IProductService productService, IClaimService claimService)
        {
            this.productService = productService;
            this.claimService = claimService;
        }

        [HttpGet("page")]
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

        [HttpPost("add")]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductCreateModel product)
        {
            var validateResult = await productService.ValidateCreateProductAsync(product);
            if (!validateResult.IsValid) return BadRequest(validateResult.Errors);
            //
            if (await productService.AddAsync(product)) return Ok(product);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = VinEcom.VINECOM_PRODUCT_CREATE_ERROR });
        }
        //[EnumAuthorize(Role.Administrator | Role.Staff | Role.Customer)]
        [HttpGet("{id?}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var role = claimService.GetRole();
            var hideBlock = role == Role.Customer;
            var result = await productService.GetProductByIdAsync(id, hideBlock);
            if (result == null) return NotFound(new { message = VinEcom.VINECOM_PRODUCT_NOT_EXIST });
            if (role == Role.Staff && result.Store.Id != claimService.GetStoreId()) return Unauthorized(new { message = VinEcom.VINECOM_UNAUTHORIZED });
            return Ok(result);
        }
    }
}
