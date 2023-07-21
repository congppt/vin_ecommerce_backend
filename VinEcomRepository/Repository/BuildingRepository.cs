using Microsoft.EntityFrameworkCore;
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
    public class BuildingRepository : BaseRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync()
        {
            var buildings = await context.Set<Building>().ToListAsync();
            return buildings.OrderBy(b => b.Name);
        }
    }
}
