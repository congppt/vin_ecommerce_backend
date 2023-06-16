using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Store;

namespace VinEcomInterface.IService
{
    public interface IStoreService : IBaseService
    {
        Task<ValidationResult> ValidateStoreRegistrationAsync(StoreRegisterViewModel vm);
        Task<bool> IsBuildingExistedAsync(int buildingId);
        Task<bool> RegisterAsync(StoreRegisterViewModel vm);
    }
}
