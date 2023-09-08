using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CustomValidators
{
    public static class StringValidator
    {
        public static IRuleBuilder<T,string> ValidateDescription<T>(this IRuleBuilder<T, string> ruleBuilder,bool isEmpty = true, int maxLength = 100)
        {
            var validator = ruleBuilder.MaximumLength(maxLength).WithMessage("{PropertyName} should not exceed {MaxLength} Charators");
            if(isEmpty == false)
            {
                validator.NotEmpty().WithMessage("{PropertyName} is required");
            }
            return validator;
        }
    }
}
