using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Pipelines.Sockets.Unofficial.Arenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomInterface.IValidator;
using VinEcomUtility.Pagination;
using VinEcomViewModel.OrderDetail;
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
            return await unitOfWork.SaveChangesAsync();
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

        public async Task<bool> ChangeBlockStatusAsync(Store store)
        {
            store.IsBlocked = !store.IsBlocked;
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdateWorkingStatus()
        {
            var storeId = claimService.GetStoreId();
            if (storeId < 0) return false;
            //
            var store = await unitOfWork.StoreRepository.GetByIdAsync(storeId);
            if (store is null) return false;
            //
            store.IsWorking = !store.IsWorking;
            unitOfWork.StoreRepository.Update(store);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<Pagination<StoreViewModel>> GetStorePagesAsync(int pageIndex, int pageSize, bool isSortDesc)
        {
            var role = claimService.GetRole();
            var result = await unitOfWork.StoreRepository.GetStorePagesAsync(pageIndex, pageSize, role != Role.Administrator);
            if (isSortDesc is true) result.Items = result.Items.OrderByDescending(x => x.Id).ToList();
            return mapper.Map<Pagination<StoreViewModel>>(result);
        }

        public async Task<Store?> FindStoreAsync(int storeId)
        {
            return await unitOfWork.StoreRepository.GetByIdAsync(storeId);
        }

        public async Task<StoreViewModel?> GetByIdAsync(int id, bool isBlocked)
        {
            var result = await unitOfWork.StoreRepository.GetStoreByIdAsync(id, isBlocked);
            return mapper.Map<StoreViewModel>(result);
        }

        public async Task<IEnumerable<StoreReviewViewModel>> GetStoreReviewAsync(bool isSortDesc)
        {
            var storeId = claimService.GetStoreId();
            var details = await unitOfWork.OrderDetailRepository.GetDetailsByStoreIdAndStatusAsync(storeId, OrderStatus.Done);
            var detailRevieweds = details.Where(x => !string.IsNullOrEmpty(x.Comment) || x.Rate.HasValue);
            if (isSortDesc is true) detailRevieweds = detailRevieweds.OrderByDescending(x => x.Id);
            return mapper.Map<IEnumerable<StoreReviewViewModel>>(detailRevieweds);
        }

        public async Task<decimal> GetStoreOrderTotalAsync(OrderStatus? status)
        {
            var total = 0m;
            var storeId = claimService.GetStoreId();
            var orders = await unitOfWork.OrderRepository.GetOrderByStoreIdAndStateAsync(storeId, status);
            //
            foreach (var order in orders)
            {
                foreach (var detail in order.Details)
                {
                    total += detail.Price.HasValue ? detail.Price.Value : 0 * detail.Quantity;
                }
            }
            //
            return total;
        }
    }
}
