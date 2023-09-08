using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CustomValidators
{
    public static class EmailValidator
    {
        public static IRuleBuilder<T,string> ValidateEmail<T>(this IRuleBuilder<T,string> ruleBuilder, bool isRequired = false)
        {
            var validator = ruleBuilder.EmailAddress().WithMessage("{PropertyName} is Not Valid");
            if(isRequired)
            {
                validator.NotEmpty().WithMessage("{PropertyName} is required");
            }
            return validator;   
        }
    }
}
