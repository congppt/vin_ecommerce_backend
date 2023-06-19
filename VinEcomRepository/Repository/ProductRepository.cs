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

        public async Task<Pagination<Product>> GetProductPagesAsync(int pageIndex, int pageSize)
        {
            var source = context.Set<Product>().AsNoTracking().Where(p => p.IsRemoved == false).AsQueryable();
            var totalCount = await source.CountAsync();
            var items = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var result = new Pagination<Product>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };
            return result;
        }

        public async Task<Pagination<Product>> GetPageByStoreIdAsync(int storeId, int pageIndex, int pageSize)
        {
            var source = context.Set<Product>().AsNoTracking()
                .Where(p => p.StoreId == storeId && p.IsRemoved == false)
                .AsQueryable();
            var totalCount = await source.CountAsync();
            var items = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var result = new Pagination<Product>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };
            //if (result.TotalPagesCount < pageIndex + 1) return await GetPageByStoreIdAsync(0, pageSize);
            return result;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Set<Product>()
                .Include(x => x.Store)
                .Include(x => x.OrderDetails)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsRemoved);
        }

        public async Task<Pagination<Product>> GetProductFiltetAsync(int pageIndex, int pageSize, ProductFilterModel filter)
        {
            var products = context.Set<Product>().AsNoTracking().AsQueryable();
            products = products.Where(x => (int)x.Category == filter.Category);
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
