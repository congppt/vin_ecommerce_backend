using AutoMapper;
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
        protected readonly IMapper mapper;

        public BaseService(IUnitOfWork unitOfWork,
                           IConfiguration config,
                           ITimeService timeService,
                           ICacheService cacheService,
                           IClaimService claimService,
                           IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.timeService = timeService;
            this.cacheService = cacheService;
            this.claimService = claimService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync()
        {
            return await unitOfWork.BuildingRepository.GetBuildingsAsync();
        }

        public async Task<Building?> GetBuildingById(int id)
        {
            return await unitOfWork.BuildingRepository.GetByIdAsync(id);
        }
    }
}
