using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomRepository;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Base;

namespace VinEcomService.Service
{
    public class BaseService : IBaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IClaimService claimService;
        protected readonly IConfiguration config;
        protected readonly ITimeService timeService;
        protected readonly ICacheService cacheService;

        public BaseService(IUnitOfWork unitOfWork, IConfiguration config,
            ITimeService timeService, ICacheService cacheService, IClaimService claimService)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.timeService = timeService;
            this.cacheService = cacheService;
            this.claimService = claimService;
        }

        public async Task<IEnumerable<Building>> GetBuildings()
        {
            return await unitOfWork.BuildingRepository.GetAllAsync();
        }
    }
}
