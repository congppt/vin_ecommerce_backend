using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomDomain.Resources;
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomInterface.IValidator;
using VinEcomUtility.Pagination;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Base;
using VinEcomViewModel.StoreStaff;

namespace VinEcomService.Service
{
    public class StoreStaffService : UserService, IStoreStaffService
    {
        public StoreStaffService(IUnitOfWork unitOfWork,
                                 IConfiguration config,
                                 ITimeService timeService,
                                 ICacheService cacheService,
                                 IClaimService claimService,
                                 IMapper mapper,
                                 IUserValidator validator) : base(unitOfWork, config, timeService, cacheService, claimService, mapper, validator)
        {
        }
        public async Task<AuthorizedViewModel?> AuthorizeAsync(SignInViewModel vm)
        {
            var storeStaff = await unitOfWork.StoreStaffRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (storeStaff is null) return null;
            string accessToken = storeStaff.User.GenerateToken(storeStaff.Id, config, timeService.GetCurrentTime(), 60 * 24 * 30, Role.Staff, storeStaff.StoreId);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken,
                UserId = storeStaff.User.Id,
                Name = storeStaff.User.Name,
                AvatarUrl = storeStaff.User.AvatarUrl,
                Email = storeStaff.User.Email,
                Phone = storeStaff.User.Phone
            };
        }

        public async Task<bool> IsStoreExistedAsync(int storeId)
        {
            return await unitOfWork.StoreRepository.GetByIdAsync(storeId) is not null;
        }

        public async Task<bool> RegisterAsync(StoreStaffSignUpViewModel vm)
        {
            var user = mapper.Map<User>(vm);
            user.PasswordHash = vm.Password.BCryptSaltAndHash();
            var staff = new StoreStaff
            {
                User = user,
                StoreId = vm.StoreId
            };
            await unitOfWork.StoreStaffRepository.AddAsync(staff);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<ValidationResult> ValidateRegistrationAsync(StoreStaffSignUpViewModel vm)
        {
            return await validator.StaffCreateValidator.ValidateAsync(vm);
        }

        #region CancelOrder
        public async Task<bool> CancelOrderAsync(Order order)
        {
            if (order is null || order.Status != OrderStatus.Preparing) return false;
            //
            order.Status = OrderStatus.Cancel;
            order.Details = null;
            unitOfWork.OrderRepository.Update(order);
            return await unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region StaffInfo
        public async Task<StoreStaffViewModel?> GetStaffInfoAsync()
        {
            var staffId = claimService.GetRoleId();
            var staff = await unitOfWork.StoreStaffRepository.GetStaffByIdAsync(staffId);
            return mapper.Map<StoreStaffViewModel>(staff);
        }
        #endregion

         #region ValidateUpdate
        public Task<ValidationResult> ValidateStaffUpdateAsync(StoreStaffUpdateViewModel vm)
        {
            return validator.StaffUpdateValidator.ValidateAsync(vm);
        }
        #endregion

        #region UpdateInfo
        public async Task<bool> UpdateStaffInfoAsync(StoreStaffUpdateViewModel vm)
        {
            var staffId = claimService.GetRoleId();
            var staff = await unitOfWork.StoreStaffRepository.GetStaffByIdAsync(staffId);
            //
            if (staff is null) return false;
            mapper.Map(vm, staff);
            //
            unitOfWork.StoreStaffRepository.Update(staff);
            return await unitOfWork.SaveChangesAsync();
        }
        #endregion

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            var storeId = claimService.GetStoreId();
            return await unitOfWork.OrderRepository.GetStoreOrderWithDetailAsync(orderId, storeId);
        }

        public async Task<Pagination<StoreStaffViewModel>> GetStaffPageAsync(int pageIndex, int pageSize)
        {
            var result = await unitOfWork.StoreStaffRepository.GetStaffPageAsync(pageIndex, pageSize);
            return mapper.Map<Pagination<StoreStaffViewModel>>(result);
        }
    }
}
