using Application.Common.CustomValidators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CategoryApp{
    public class UpsertCategoryCommandValidator: AbstractValidator<UpsertCategoryCommand>
    {
        public UpsertCategoryCommandValidator()
        {
            RuleFor(p => p.Name).ValidateName();
        }
    }
}
