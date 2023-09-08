using Application.Common.CustomValidators;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.UpsertOffer
{
    public class UpsertOfferCommandValidator : AbstractValidator<UpsertOfferCommand>
    {
        public UpsertOfferCommandValidator()
        {
            RuleFor(p => p.Id);

            RuleFor(p => p.OwnerName).ValidateMaxLength(true, 30);

            RuleFor(p => p.Phone1).ValidatePhone(true);

            RuleFor(p => p.Phone2).NotNull();

            RuleFor(p => p.DepartmentId).ValidateIntKey();

            RuleFor(p => p.PurposeId).ValidateIntKey();

            RuleFor(p => p.TypeId).ValidateIntKey();

            RuleFor(p => p.AreaId).ValidateIntKey();

            RuleFor(p => p.LocationId).ValidateIntKey();

            RuleFor(p => p.SectionId).ValidateIntKey();

            RuleFor(p => p.DistributionId).ValidateIntKey();

            RuleFor(p => p.Price);

            RuleFor(p => p.Kasema);

            RuleFor(p => p.Piece);

            RuleFor(p => p.House);

            RuleFor(p => p.Street);

            RuleFor(p => p.Details);

            RuleFor(p => p.Photos);
        }
    
    }
}
