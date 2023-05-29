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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<Pagination<Product>> GetProductPage(int pageIndex = 0, int pageSize = 10)
        {
            return unitOfWork.ProductRepository.GetPageAsync(pageIndex, pageSize);
        }
    }
}
