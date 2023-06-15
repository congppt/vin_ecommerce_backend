using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomInterface.IRepository;

namespace VinEcomInterface
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IProductRepository ProductRepository { get; }
        IShipperRepository ShipperRepository { get; }
        IStoreStaffRepository StoreStaffRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IBuildingRepository BuildingRepository { get; }
        IUserRepository UserRepository { get; }
        IStoreRepository StoreRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
