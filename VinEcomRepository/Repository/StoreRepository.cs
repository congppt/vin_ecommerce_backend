using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomDomain.Model;
using VinEcomInterface.IRepository;

namespace VinEcomRepository.Repository
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        public StoreRepository(AppDbContext context) : base(context)
        {
        }
    }
}
