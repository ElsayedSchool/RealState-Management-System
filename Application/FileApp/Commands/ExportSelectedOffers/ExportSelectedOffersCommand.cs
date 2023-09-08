using Application.Common.Mediatr;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileApp.Commands.ExportSelectedOffers
{
    public class ExportSelectedOffersCommand : IRequestWrapper<string>
    {
        public List<int> selectedOffers { get; set; } = new List<int>();
    }

    public class ExportSelectedOffersCommandValidator : AbstractValidator<ExportSelectedOffersCommand>
    {
        public ExportSelectedOffersCommandValidator()
        {
            RuleForEach(p => p.selectedOffers).GreaterThanOrEqualTo(1).WithMessage("من فضلك ارسل بيانات العروض صحيحه!");
        }
    }
}
