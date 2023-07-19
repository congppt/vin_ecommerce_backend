using FluentValidation;
using VinEcomViewModel.Product;

namespace VinEcomInterface.IValidator
{
    public interface IProductValidator
    {
        IValidator<ProductCreateModel> ProductCreateValidator { get; }
        IValidator<StoreProductFilterViewModel> StoreProductFilterValidator { get; }
        IValidator<ProductFilterModel> ProductFilterValidator { get; }
        IValidator<ProductUpdateViewModel> ProductUpdateValidator { get; }
    }
}
