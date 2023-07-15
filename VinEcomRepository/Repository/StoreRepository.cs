using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomDomain.Model;
using VinEcomInterface.IRepository;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Store;

namespace VinEcomRepository.Repository
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Pagination<Store>> FilterStoreAsync(StoreFilterViewModel vm)
        {
            var source = context.Set<Store>().AsNoTracking().Where(s => !s.IsBlocked);
            if (!string.IsNullOrWhiteSpace(vm.Name)) source = source.Where(s => s.Name.ToLower().Contains(vm.Name.ToLower()));
            if (vm.Category.HasValue) source = source.Where(s => s.Category == vm.Category);
            var totalCount = await source.CountAsync();
            var items = await source.Skip(vm.PageIndex * vm.PageSize).Take(vm.PageSize).ToListAsync();
            var result = new Pagination<Store>()
            {
                Items = items,
                PageIndex = vm.PageIndex,
                PageSize = vm.PageSize,
                TotalItemsCount = totalCount
            };
            //if (result.TotalPagesCount < pageIndex + 1) return await GetPageByStoreIdAsync(0, pageSize);
            return result;
        }

        public async Task<Store?> GetStoreByIdAsync(int id, bool isBlocked)
        {
            return await context.Set<Store>()
                .AsNoTracking()
                .Include(x => x.Building).FirstOrDefaultAsync(x => x.Id == id && x.IsBlocked == isBlocked);
        }

        public async Task<Pagination<Store>> GetStorePagesAsync(int pageIndex, int pageSize, bool hideBlocked)
        {
            var source = context.Set<Store>()
                .Include(x => x.Building)
                .AsNoTracking();
            if (hideBlocked) source = source.Where(x => x.IsBlocked);
            //
            var totalCount = await source.CountAsync();
            var items = await source.Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
            //
            var result = new Pagination<Store>()
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
