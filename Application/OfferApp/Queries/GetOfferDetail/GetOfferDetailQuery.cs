using Application.Common.CustomValidators;
using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Queries.GetOfferDetail
{
    public class GetOfferDetailQuery:IRequestWrapper<OfferDetailVm>
    {
        public int OfferId { get; set; }
    }

    public class GetOfferDetailQueryValidator : AbstractValidator<GetOfferDetailQuery>
    {
        public GetOfferDetailQueryValidator() 
        { 
            RuleFor(p => p.OfferId).GreaterThanOrEqualTo(1).WithMessage("لا توجد بيانات متاحه للعرض");      
        }
    }
}
