using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomInterface.IValidator;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Store;

namespace VinEcomService.Service
{
    public class StoreService : BaseService, IStoreService
    {
        private readonly IStoreValidator validator;
        public StoreService(IUnitOfWork unitOfWork,
                            IConfiguration config,
                            ITimeService timeService,
                            ICacheService cacheService,
                            IClaimService claimService,
                            IMapper mapper,
                            IStoreValidator validator) : base(unitOfWork, config, timeService, cacheService, claimService, mapper)
        {
            this.validator = validator;
        }

        public Task<ValidationResult> ValidateStoreRegistrationAsync(StoreRegisterViewModel vm)
        {
            return validator.StoreCreateValidator.ValidateAsync(vm);
        }
        public async Task<bool> IsBuildingExistedAsync(int buildingId)
        {
            return await unitOfWork.BuildingRepository.GetByIdAsync(buildingId) is not null;
        }

        public async Task<bool> RegisterAsync(StoreRegisterViewModel vm)
        {
            var store = mapper.Map<Store>(vm);
            await unitOfWork.StoreRepository.AddAsync(store);
            if (await unitOfWork.SaveChangesAsync()) return true;
            return false;
        }

        public async Task<ValidationResult> ValidateStoreFilterAsync(StoreFilterViewModel vm)
        {
            return await validator.StoreFilterValidator.ValidateAsync(vm);
        }

        public async Task<Pagination<StoreFilterResultViewModel>> GetStoreFilterResultAsync(StoreFilterViewModel vm)
        {
            var stores = await unitOfWork.StoreRepository.FilterStoreAsync(vm);
            var result = mapper.Map<Pagination<StoreFilterResultViewModel>>(stores);
            return result;
        }

        #region UpdateWorkingStatus
        public async Task<bool> UpdateWorkingStatusAsync(int storeId)
        {
            var store = await unitOfWork.StoreRepository.GetByIdAsync(storeId);
            if (store is null) return false;
            var currentStatus = store.IsWorking;
            store.IsWorking = !currentStatus;
            unitOfWork.StoreRepository.Update(store);
            return await unitOfWork.SaveChangesAsync();
        }
        #endregion
    }
}
