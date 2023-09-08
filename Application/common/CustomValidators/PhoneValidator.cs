using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CustomValidators
{
    public static class PhoneValidator
    {
        public static IRuleBuilder<T,string> ValidatePhone<T>(this IRuleBuilder<T,string> ruleBuilder, bool isRequired = true,int maxLength = 13)
        {
            var validator = ruleBuilder
                .Length(maxLength).WithMessage("{PropertyName} should be exactly {MaxLength}");
            if(isRequired)
            {
                validator.NotEmpty().WithMessage("{PropertyName} is required");
            }
            return validator;
        }
    }
}
