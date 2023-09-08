using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FavoriteApp.Commands.IsFavorite
{
    public class IsFavoriteCommand : IRequestWrapper<bool>
    {
        public int offerId { get; set; }
    }

    public class IsFavoriteCommandValidator : AbstractValidator<IsFavoriteCommand>
    {
        public IsFavoriteCommandValidator()
        {
            RuleFor(p => p.offerId).GreaterThanOrEqualTo(1).WithMessage("من فضلك ارسل بيانات صحيحه");
        }
    }
}
