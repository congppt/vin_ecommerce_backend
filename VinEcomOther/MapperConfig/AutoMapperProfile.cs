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
using VinEcomViewModel.Product;
using VinEcomViewModel.Shipper;
using VinEcomViewModel.Store;

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
            CreateMap<Store, StoreFilterResultViewModel>();
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.User.IsBlocked))
                .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building));
            //Order
            CreateMap<Order, OrderWithDetailsViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.User.Name))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.User.Phone))
                .ForMember(dest => dest.FromBuildingName, opt => opt.MapFrom(src => src.Details.First().Product.Store.Building.Name))
                .ForMember(dest => dest.FromBuildingLocation, opt => opt.MapFrom(src => src.Details
                .First().Product.Store.Building.Latitude + "," + src.Details.First().Product.Store.Building.Longitude))
                .ForMember(dest => dest.ToBuildingName, opt => opt.MapFrom(src => src.Building.Name))
                .ForMember(dest => dest.ToBuildingLocation, opt => opt.MapFrom(src => src.Building.Latitude + "," + src.Building.Longitude))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Details.First().Product.Store.Name))
                .ForMember(dest => dest.StoreImageUrl, opt => opt.MapFrom(src => src.Details.First().Product.Store.ImageUrl));
            //OrderDetail
            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            //Shipper
            CreateMap<Shipper, ShipperViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.VehicleType.GetDisplayName()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()));
            CreateMap<Building, BuildingBasicViewModel>();
            CreateMap<CustomerUpdateBasicViewModel, User>();
            CreateMap<CustomerUpdateBasicViewModel, Customer>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src));
            //Store
            CreateMap<Store, StoreViewModel>()
                .ForMember(dest => dest.BuidingName, opt => opt.MapFrom(src => src.Building.Name));
        }
    }
}
