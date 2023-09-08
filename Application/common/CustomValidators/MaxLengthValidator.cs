using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CustomValidators
{
    public static class MaxLengthValidator
    {
        public static IRuleBuilder<T, string> ValidateMaxLength<T>(this IRuleBuilder<T, string> ruleBuilder, bool isRequired = false, int maxLength = 50)
        {
            var validator = ruleBuilder.MaximumLength(maxLength).WithMessage("{PropertyName} should not exceed {MaxLength}");
            if (isRequired)
            {
                validator.NotEmpty().WithMessage("{PropertyName} is required");
            }
            return validator;
        }
    }
}
