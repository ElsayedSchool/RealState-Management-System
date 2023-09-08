using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FavoriteApp.Commands.UpsertFavorite
{
    public class UpsertFavoriteCommand : IRequestWrapper<bool>
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public bool IsFavorite { get; set; }
    }

    class UpsertfavoriteCommandValidator : AbstractValidator<UpsertFavoriteCommand>
    {
        public UpsertfavoriteCommandValidator()
        {

            RuleFor(p => p.OfferId).GreaterThanOrEqualTo(1).WithMessage("من فضلك ارسل بيانات العرض صحيحه");
        }
    }
}
