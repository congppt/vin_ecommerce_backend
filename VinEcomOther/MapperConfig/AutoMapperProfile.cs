using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //
            CreateMap<ProductCreateModel, Product>();
            CreateMap<Store, StoreBasicViewModel>();
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.GetDisplayName()))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store));
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
            CreateMap<Order, OrderWithDetailsViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.User.Name))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.User.Phone))
                .ForMember(dest => dest.FromBuildingName, opt => opt.MapFrom(src => src.Details.First().Product.Store.Building.Name))
                .ForMember(dest => dest.FromBuildingLocation, opt => opt.MapFrom(src => src.Details
                .First().Product.Store.Building.Latitude + "," + src.Details.First().Product.Store.Building.Longitude))
                .ForMember(dest => dest.ToBuildingName, opt => opt.MapFrom(src => src.Building.Name))
                .ForMember(dest => dest.ToBuildingLocation, opt => opt.MapFrom(src => src.Building.Latitude + "," + src.Building.Longitude))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Details.First().Product.Store.Name))
                .ForMember(dest => dest.StoreImageUrl, opt => opt.MapFrom(src => src.Details.First().Product.Store.ImageUrl));
            CreateMap<Order, OrderBasicViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
                .ForMember(dest => dest.FromBuilding, opt => opt.MapFrom(src => src.Details.First().Product.Store.Building))
                .ForMember(dest => dest.ToBuilding, opt => opt.MapFrom(src => src.Building))
                .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Details.First().Product.Store))
                .ForMember(dest => dest.Shipper, opt => opt.MapFrom(src => src.Shipper))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details));
            //OrderDetail
            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Order.Customer));
            CreateMap<OrderDetail, OrderDetailBasicViewModel>();
            //Shipper
            CreateMap<Shipper, ShipperViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType.GetDisplayName()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()));
            CreateMap<Building, BuildingBasicViewModel>();
            //Store
            CreateMap<Store, StoreViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.GetDisplayName()));
            CreateMap<OrderDetail, StoreReviewViewModel>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Order.Customer.User.Name))
                .ForMember(dest => dest.CustomerAvatarUrl, opt => opt.MapFrom(src => src.Order.Customer.User.AvatarUrl));
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
