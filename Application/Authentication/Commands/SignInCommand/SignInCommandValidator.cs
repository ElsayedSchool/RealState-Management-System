using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class AddCenterCommandValidator : AbstractValidator<SignInCommand>
    {
        public AddCenterCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(10).WithMessage("Username or Password is not valid");
        }
    }
}
