using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomViewModel.Store;

namespace VinEcomInterface.IValidator
{
    public interface IStoreValidator
    {
        IValidator<StoreRegisterViewModel> StoreCreateValidator { get; }
        IValidator<StoreFilterViewModel> StoreFilterValidator { get; }
    }
}
