using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomUtility.UtilityMethod;
using VinEcomViewModel.Base;
using VinEcomViewModel.Building;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Order;
using VinEcomViewModel.OrderDetail;
using VinEcomViewModel.Product;
using VinEcomViewModel.Shipper;
using VinEcomViewModel.Store;
using VinEcomViewModel.StoreStaff;

namespace VinEcomOther.MapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            //Product
            CreateMap<ProductCreateModel, Product>();
            CreateMap<Store, StoreBasicViewModel>();
            CreateMap<ProductCategory, ProductCategoryViewModel>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => (int)src))
                                                                  .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.GetDisplayName()));
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store));
            CreateMap<ProductUpdateViewModel, Product>();
            //
            CreateMap<SignUpViewModel, User>();
            CreateMap<StoreRegisterViewModel, Store>();
            //Customer
            CreateMap<Store, StoreFilterResultViewModel>();
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.User.IsBlocked))
                .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building));
            CreateMap<Customer, CustomerBasicViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl));
            CreateMap<CustomerUpdateBasicViewModel, User>();
            CreateMap<CustomerUpdateBasicViewModel, Customer>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
            //Order
            CreateMap<OrderStatus, OrderStatusViewModel>().ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => (int)src))
                                                          .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.GetDisplayName()));
            CreateMap<Order, OrderWithDetailsViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.FromBuilding, opt => opt.MapFrom(src => src.Details.First().Product.Store.Building))
                .ForMember(dest => dest.ToBuilding, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Details.First().Product.Store))
                .ForMember(dest => dest.Shipper, opt => opt.MapFrom(src => src.Shipper));
            CreateMap<Order, OrderStoreViewModel>()
                .ForMember(dest => dest.CustomerBuilding, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.StoreBuilding, opt => opt.MapFrom(src => src.Details.First().Product.Store.Building))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.User.Name))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Details.Sum(x => x.Price * x.Quantity)));
            //OrderDetail
            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Order.Customer));
            CreateMap<OrderDetail, OrderDetailBasicViewModel>();
            CreateMap<OrderDetailRatingViewModel, OrderDetail>();
            //Shipper
            CreateMap<Shipper, ShipperViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType.GetDisplayName()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()));
            CreateMap<ShipperUpdateBasicViewModel, User>();
            CreateMap<ShipperUpdateBasicViewModel, Shipper>().ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
            CreateMap<Building, BuildingBasicViewModel>();
            //Store
            CreateMap<StoreCategory, StoreCategoryViewModel>().ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => (int)src))
                                                              .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.GetDisplayName()));
            CreateMap<Store, StoreViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<OrderDetail, StoreReviewViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Order.Customer));
            //Staff
            CreateMap<StoreStaff, StoreStaffViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<StoreStaffUpdateViewModel, User>();
            CreateMap<StoreStaffUpdateViewModel, StoreStaff>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
        }
    }
}
