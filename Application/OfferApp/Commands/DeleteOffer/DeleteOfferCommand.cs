using Application.Common.Mediatr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.DeleteOffer
{
    public class DeleteOfferCommand  :IRequestWrapper<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteOfferValidator : AbstractValidator<DeleteOfferCommand>
    {
        public DeleteOfferValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("!من فضلك ارسل البيانات المطلوبه");
        }
    }
}
