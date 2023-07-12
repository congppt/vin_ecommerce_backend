using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public async Task<Pagination<Product>> GetStoreProductPageAsync(StoreProductFilterViewModel vm)
        {
            var source = context.Set<Product>()
                .Include(x => x.Store)
                .AsNoTracking().Where(p => p.StoreId == vm.StoreId && !p.IsRemoved && !p.Store.IsBlocked);
            var totalCount = await source.CountAsync();
            var items = await source.Skip(vm.PageIndex * vm.PageSize).Take(vm.PageSize).ToListAsync();
            var result = new Pagination<Product>()
            {
                Items = items,
                PageIndex = vm.PageIndex,
                PageSize = vm.PageSize,
                TotalItemsCount = totalCount
            };
            //if (result.TotalPagesCount < pageIndex + 1) return await GetPageAsync(0, pageSize);
            return result;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Set<Product>()
                .AsNoTracking()
                .Include(x => x.Store)
                .Include(x => x.OrderDetails)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsRemoved && !x.Store.IsBlocked);
        }

        public async Task<Product?> GetProductByIdNoTrackingAsync(int id)
        {
            return await context.Set<Product>()
                .AsNoTracking()
                .Include(x => x.OrderDetails)
                .FirstOrDefaultAsync(x => x.Id == id
                && !x.IsRemoved
                && !x.IsOutOfStock
                && !x.Store.IsBlocked);
        }

        public async Task<Pagination<Product>> GetProductByCategoryAsync(ProductCategory category, int pageIndex, int pageSize)
        {
            var products = context.Set<Product>()
                .Include(x => x.Store)
                .AsNoTracking()
                .Where(x => x.Category == category && !x.IsRemoved && !x.Store.IsBlocked);
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

        public async Task<Pagination<Product>> GetProductPagesByNameAsync(string name, int pageIndex, int pageSize)
        {
            var products = context.Set<Product>()
                .Include(x => x.Store)
                .AsNoTracking()
                .Where(x => x.Name.ToLower().Contains(name.ToLower()) && !x.IsRemoved && !x.Store.IsBlocked);
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

        public async Task<Pagination<Product>> GetProductPagingAsync(int pageIndex, int pageSize)
        {
            var products = context.Set<Product>()
               .AsNoTracking()
               .Include(x => x.Store)
               .Where(x => !x.IsRemoved && !x.Store.IsBlocked);
            var totalCount = await products.CountAsync();
            var items = await products.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var result = new Pagination<Product>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };

            return result;
        }

        public async Task<Product?> GetProductByIdAsync(int id, bool hideBlocked)
        {
            var product = await context.Set<Product>().AsNoTracking().Include(p => p.Store).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;
            if (!hideBlocked) return product;
            return product.IsRemoved ? null : product;
        }
    }
}
