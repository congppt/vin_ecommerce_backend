using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface;
using VinEcomInterface.IService;

namespace VinEcomService.Service
{
    public class StoreService : BaseService, IStoreService
    {
        public StoreService(IUnitOfWork unitOfWork,
                            IConfiguration config,
                            ITimeService timeService,
                            ICacheService cacheService,
                            IClaimService claimService,
                            IMapper mapper) : base(unitOfWork, config, timeService, cacheService, claimService, mapper)
        {
        }
    }
}
