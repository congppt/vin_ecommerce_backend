using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Model;
using VinEcomViewModel.Base;
using VinEcomViewModel.Product;
using VinEcomViewModel.Store;

namespace VinEcomOther.MapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductCreateModel, Product>();
            CreateMap<SignUpViewModel, User>();
            CreateMap<StoreRegisterViewModel, Store>();
        }
    }
}
