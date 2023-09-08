using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CustomValidators
{
    public static class IntKeyValidator
    {
        public static IRuleBuilder<T,int> ValidateIntKey<T>(this IRuleBuilder<T,int> ruleBuilder)
        {
            return ruleBuilder.GreaterThanOrEqualTo(1).WithMessage("من فضلك ارسل بيانات صحيحه!");
        }
    }
}
