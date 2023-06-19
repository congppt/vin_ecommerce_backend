using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Resources;

namespace VinEcomViewModel.Product
{
    public class StoreProductFilterViewModel
    {
        public int StoreId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class StoreProductFilterValidator : AbstractValidator<StoreProductFilterViewModel>
    {
        public StoreProductFilterValidator(){
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0).WithMessage(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(VinEcom.VINECOM_PAGE_SIZE_ERROR);
        }
    }
}
