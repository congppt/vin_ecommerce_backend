using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;

namespace VinEcomInterface.IService
{
    public interface IProductService
    {
        Task<Pagination<Product>> GetProductPage(int pageIndex = 0, int pageSize = 10);
    }
}
