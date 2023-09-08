using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Username should not be empty.")
                .MinimumLength(10).WithMessage("Username should be at least 10 charactor.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone Number should not be empty.")
                .MinimumLength(8).WithMessage("Phone Number should be at least 8 charactor.")
                .MaximumLength(12).WithMessage("Phone Number should not exceed 12 charactor");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password Should not be empty.")
                .MinimumLength(10).WithMessage("Password should be at least 10 charactor.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email should not be empty.")
                .EmailAddress().WithMessage("Email should be valid email Form.");
        }
    }
}
