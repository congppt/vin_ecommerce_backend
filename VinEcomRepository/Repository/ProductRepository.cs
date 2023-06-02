using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomInterface.IRepository;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Product;

namespace VinEcomRepository.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        { }

        public async Task<Pagination<Product>> GetProductFiltetAsync(int pageIndex, int pageSize, ProductFilterModel filter)
        {
            var products = context.Set<Product>().AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(filter.Category))
            {
                var enumValue = (ProductCategory) Enum.Parse(typeof(ProductCategory), filter.Category);
                products = products.Where(x => x.Category == enumValue);
            }
            var totalCount = await products.CountAsync();
            var items = await products.AsNoTracking().Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var result = new Pagination<Product>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }
    }
}
