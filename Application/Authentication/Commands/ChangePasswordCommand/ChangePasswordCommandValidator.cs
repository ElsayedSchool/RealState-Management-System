using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User data should be valid");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current Password should not be empty.")
                .MinimumLength(10).WithMessage("Current Password is not valid.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password should not be empty.")
                .MinimumLength(10).WithMessage("New Password is not valid.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password should not be empty.")
                .MinimumLength(10).WithMessage("Confirm Password is not valid.")
                .Equal<ChangePasswordCommand>(x => x.NewPassword)
                .WithMessage("New Password is not matching with old password.");

        }
    }
}
