using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CustomValidators
{
    public static class GreaterValidator
    {
        public static IRuleBuilder<T,int> ValidateGreaterThan<T>(this IRuleBuilder<T,int> ruleBuilder, int minValue = 0)
        {
            return ruleBuilder.GreaterThan(minValue).WithMessage("{PropertyName} shoulb be greater than minValue");
        }
    }
}
