using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.Commands.AddSharedOffers
{
    public class AddSharedOffersCommand : IRequestWrapper<bool>
    {
        public int Id { get; set; }
        public List<string> SharedToList { get; set; } = new List<string>();
        public List<int> Offers { get; set; } = new List<int>();
    }

    public class AddSharedCommandValidator : AbstractValidator<AddSharedOffersCommand>
    {
        public AddSharedCommandValidator()
        {
            RuleForEach(p => p.Offers).GreaterThanOrEqualTo(1).WithMessage("يجب ارسال بيانات العروض صحيحه");

            RuleForEach(p => p.SharedToList).NotEmpty().WithMessage("يجب ارسال بيانات الموظفين صحيحه");
        }
    }
}
