using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinEcomDomain.Enum;
using VinEcomDomain.Resources;

namespace VinEcomViewModel.Store
{
    public class StoreFilterViewModel
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Name { get; set; }
        public StoreCategory? Category { get; set; }
    }
    public class StoreFilterValidator : AbstractValidator<StoreFilterViewModel>
    {
        public StoreFilterValidator()
        {
            RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0).WithMessage(VinEcom.VINECOM_PAGE_INDEX_ERROR);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(VinEcom.VINECOM_PAGE_SIZE_ERROR);
            RuleFor(x => x.Category).IsInEnum().When(x => x.Category.HasValue).WithMessage(VinEcom.VINECOM_STORE_CATEGORY_NOT_EXIST_ERROR);
        }
    }
}
