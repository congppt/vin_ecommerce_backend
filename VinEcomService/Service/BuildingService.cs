using AutoMapper;
using Microsoft.Extensions.Configuration;
using VinEcomDomain.Model;
using VinEcomInterface;
using VinEcomInterface.IService;

namespace VinEcomService.Service
{
    public class BuildingService : BaseService, IBuildingService
    {
        public BuildingService(IUnitOfWork unitOfWork, IConfiguration config, 
            ITimeService timeService, ICacheService cacheService, 
            IClaimService claimService, IMapper mapper) : 
            base(unitOfWork, config, timeService, 
                cacheService, claimService, mapper)
        { }

        public async Task<Building?> GetBuildingById(int id)
        {
            return await unitOfWork.BuildingRepository.GetByIdAsync(id);
        }
    }
}
