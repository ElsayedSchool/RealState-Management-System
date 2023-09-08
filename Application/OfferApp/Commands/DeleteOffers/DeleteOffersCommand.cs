using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.DeleteOffers
{
    public class DeleteOffersCommand : IRequestWrapper<bool>
    {
        public List<int> Offers { get; set; }
    }

    class DeleteOffersCommandValidator : AbstractValidator<DeleteOffersCommand>
    {
        public DeleteOffersCommandValidator()
        {
            RuleForEach(p => p.Offers).GreaterThanOrEqualTo(1).WithMessage("من فضلك ارسل بيانات العروض صحيحه");
        }
    }
}
