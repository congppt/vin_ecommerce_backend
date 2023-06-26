using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomUtility.Pagination;
using VinEcomViewModel.Base;
using VinEcomViewModel.Customer;
using VinEcomViewModel.Order;
using VinEcomViewModel.Product;
using VinEcomViewModel.Store;

namespace VinEcomOther.MapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            CreateMap<ProductCreateModel, Product>();
            CreateMap<SignUpViewModel, User>();
            CreateMap<StoreRegisterViewModel, Store>();
            CreateMap<Store, StoreFilterResultViewModel>();
            CreateMap<Customer, CustomerViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.User.AvatarUrl))
                .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.User.IsBlocked));
            //Order
            CreateMap<Order, OrderWithDetailsViewModel>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer.User.Name))
                .ForMember(dest => dest.Shipper, opt => opt.MapFrom(src => src.Shipper.User.Name));
            //OrderDetail
            CreateMap<OrderDetail, OrderDetailViewModel>();
        }
    }
}
