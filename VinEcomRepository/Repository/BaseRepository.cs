using Microsoft.EntityFrameworkCore;
using VinEcomDbContext;
using VinEcomInterface.IRepository;
using VinEcomUtility.Pagination;

namespace VinEcomRepository.Repository
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        protected readonly AppDbContext context;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(TModel entity)
        {
            await context.Set<TModel>().AddAsync(entity);
        }

        public void Delete(TModel entity)
        {
            context.Set<TModel>().Remove(entity);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await context.Set<TModel>().AsNoTracking().ToListAsync();
        }
        public async Task<TModel?> GetByIdAsync(int id)
        {
            return await context.Set<TModel>().FindAsync(id);
        }

        public async Task<Pagination<TModel>> GetPageAsync(int pageIndex, int pageSize)
        {
            var totalCount = await context.Set<TModel>().CountAsync();
            var items = await context.Set<TModel>().AsNoTracking().Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var result = new Pagination<TModel>()
            {
                Items = items,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = totalCount
            };
            //if (result.TotalPagesCount < pageIndex + 1) return await GetPageByStoreIdAsync(0, pageSize);
            return result;
        }

        public void Update(TModel entity)
        {
            context.Set<TModel>().Update(entity);
        }
    }
}