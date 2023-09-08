using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.ChangeDepartment
{
    public class ChangeDepartmentCommand : IRequestWrapper<bool>
    {
        public int NewDepartmentId { get; set; }
        public List<int> Offers { get; set; } = new List<int>();
    }

    public class ChangeDepartmentCommandValidator : AbstractValidator<ChangeDepartmentCommand>
    {
        public ChangeDepartmentCommandValidator()
        {
            RuleFor(p => p.NewDepartmentId).GreaterThanOrEqualTo(1).WithMessage("من فضلك ارسل بيانات القسم صحيحه");

            //RuleFor(p => p.Offers).ForEach(p => p.GreaterThanOrEqualTo(1)).WithMessage("يجب ارسال بيانات العروض صحيحه");
        }
    }
}
