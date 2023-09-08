using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.DeactivateUserCommand
{
    public class DeactivateUserCommandValidator : AbstractValidator<DeactivateUserCommand>
    {
        public DeactivateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Please Send User Id");
            RuleFor(x => x.Deactivate).NotNull().WithMessage("Please Send Required Action");
        }
    }
}
