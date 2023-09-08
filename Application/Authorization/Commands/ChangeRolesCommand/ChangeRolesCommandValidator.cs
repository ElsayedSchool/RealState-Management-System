using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Commands.ChangeRolesCommand
{
    public class ChangeRolesCommandValidator : AbstractValidator<ChangeRolesCommand>
    {
        public ChangeRolesCommandValidator() 
        { 
            RuleFor(x => x.Id).NotEmpty().WithMessage("Please Send USer Data");
            RuleFor(x => x.Roles).Null().WithMessage("Send User Authorities");
        }
    }
}
