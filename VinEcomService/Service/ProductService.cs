using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomUtility.Pagination;

namespace VinEcomService.Service
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IConfiguration config, ITimeService timeService, ICacheService cacheService, IClaimService claimService) : base(unitOfWork, config, timeService, cacheService, claimService)
        {
        }

        public Task<Pagination<Product>> GetProductPageAsync(int pageIndex = 0, int pageSize = 10)
        {
            return unitOfWork.ProductRepository.GetPageAsync(pageIndex, pageSize);
        }
    }
}
