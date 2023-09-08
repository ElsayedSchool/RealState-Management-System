using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CustomValidators
{
    public static class NullKeyVallidator
    {
        public static IRuleBuilder<T, int?> ValidateIntKey<T>(this IRuleBuilder<T, int?> ruleBuilder)
        {
            return ruleBuilder.Must(p => p.Value != null && p.Value > 0).WithMessage("{PropertyName} is not valid");
        }
    }
}
