using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.OrderDetail;
using VinEcomViewModel.Store;

namespace VinEcomInterface.IService
{
    public interface IStoreService : IBaseService
    {
        Task<ValidationResult> ValidateStoreRegistrationAsync(StoreRegisterViewModel vm);
        Task<bool> IsBuildingExistedAsync(int buildingId);
        Task<Store?> FindStoreAsync(int storeId);
        Task<bool> RegisterAsync(StoreRegisterViewModel vm);
        Task<ValidationResult> ValidateStoreFilterAsync(StoreFilterViewModel vm);
        Task<Pagination<StoreFilterResultViewModel>> GetStoreFilterResultAsync(StoreFilterViewModel vm);
        Task<bool> ChangeBlockStatusAsync(Store store);
        Task<bool> UpdateWorkingStatus();
        Task<Pagination<StoreViewModel>> GetStorePagesAsync(int pageIndex, int pageSize, bool isSortDesc);
        Task<StoreViewModel?> GetByIdAsync(int id, bool isBlocked);
        Task<IEnumerable<StoreReviewViewModel>> GetStoreReviewAsync(bool isSortDesc);
        Task<decimal> GetStoreOrderTotalAsync(OrderStatus? status);
    }
}
