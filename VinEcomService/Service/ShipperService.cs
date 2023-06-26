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
using VinEcomInterface;
using VinEcomInterface.IService;
using VinEcomInterface.IValidator;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Base;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Shipper;

namespace VinEcomService.Service
{
    public class ShipperService : UserService, IShipperService
    {
        public ShipperService(IUnitOfWork unitOfWork,
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
            var shipper = await unitOfWork.ShipperRepository.AuthorizeAsync(vm.Phone, vm.Password);
            if (shipper is null) return null;
            string accessToken = shipper.User.GenerateToken(config, timeService.GetCurrentTime(), 60 * 24 * 30, Role.Shipper);
            return new AuthorizedViewModel
            {
                AccessToken = accessToken
            };
        }
        public async Task<bool> RegisterAsync(ShipperSignUpViewModel vm)
        {
            var user = mapper.Map<User>(vm);
            user.PasswordHash = vm.Password.BCryptSaltAndHash();
            var shipper = new Shipper
            {
                User = user,
                LicensePlate = vm.LicensePlate,
                VehicleType = vm.VehicleType
            };
            await unitOfWork.ShipperRepository.AddAsync(shipper);
            if (await unitOfWork.SaveChangesAsync()) return true;
            return false;
        }

        public async Task<ValidationResult> ValidateRegistrationAsync(ShipperSignUpViewModel vm)
        {
            return await validator.ShipperCreateValidator.ValidateAsync(vm);
        }

        public async Task<IEnumerable<Shipper>> GetShippersAvailableAsync()
        {
            return await unitOfWork.ShipperRepository.GetAvailableShipperAsync();
        }

        public async Task<bool> ChangeWorkingStatusAsync()
        {
            var shipper = await FindShipperAsync();
            if (shipper == null) return false;
            shipper.Status = shipper.Status == ShipperStatus.Offline ? ShipperStatus.Available : ShipperStatus.Offline;
            unitOfWork.ShipperRepository.Update(shipper);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> OrderDeliveredAsync()
        {
            var shipper = await FindShipperAsync();
            if (shipper == null) return false;
            shipper.Status = ShipperStatus.Available;
            unitOfWork.ShipperRepository.Update(shipper);
            return await unitOfWork.SaveChangesAsync();
        }

        private async Task<Shipper?> FindShipperAsync()
        {
            var userId = claimService.GetCurrentUserId();
            return await unitOfWork.ShipperRepository.GetShipperByUserId(userId);
        }

        public async Task<IEnumerable<Order>?> GetDeliveredListAsync()
        {
            var shipper = await FindShipperAsync();
            if (shipper is null) return null;
            return shipper.Orders;
        }
    }
}
