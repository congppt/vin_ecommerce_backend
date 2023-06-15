using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDbContext;
using VinEcomInterface;
using VinEcomInterface.IRepository;

namespace VinEcomRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IStoreStaffRepository staffRepository;
        private readonly IShipperRepository shipperRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IBuildingRepository buildingRepository;
        private readonly IUserRepository userRepository;
        private readonly IStoreRepository storeRepository;
        public UnitOfWork(AppDbContext context,
                          ICustomerRepository customerRepository,
                          IOrderRepository orderRepository,
                          IProductRepository productRepository,
                          IStoreStaffRepository staffRepository,
                          IShipperRepository shipperRepository,
                          IOrderDetailRepository orderDetailRepository,
                          IBuildingRepository buildingRepository,
                          IUserRepository userRepository,
                          IStoreRepository storeRepository)
        {
            this.context = context;
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.staffRepository = staffRepository;
            this.shipperRepository = shipperRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.buildingRepository = buildingRepository;
            this.userRepository = userRepository;
            this.storeRepository = storeRepository;
        }
        public ICustomerRepository CustomerRepository => customerRepository;

        public IProductRepository ProductRepository => productRepository;

        public IShipperRepository ShipperRepository => shipperRepository;

        public IStoreStaffRepository StoreStaffRepository => staffRepository;

        public IOrderRepository OrderRepository => orderRepository;

        public IOrderDetailRepository OrderDetailRepository => orderDetailRepository;

        public IBuildingRepository BuildingRepository => buildingRepository;

        public IUserRepository UserRepository => userRepository;

        public IStoreRepository StoreRepository => storeRepository;

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
