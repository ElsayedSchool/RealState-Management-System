using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CustomValidators
{
    public static class NameValidator
    {
        public static IRuleBuilderOptions<T, string> ValidateName<T>(this IRuleBuilder<T, string> ruleBuilder, bool isEmpty = false,int maxLength = 30)
        {
            var validator =  ruleBuilder
                .MaximumLength(maxLength).WithMessage("{PropertyName} should not exceed {MaxLength} Charators");
            if(isEmpty == false)
            {
                validator.NotEmpty().WithMessage("{PropertyName} is required");
            }
            return validator;
        }
    }
}
