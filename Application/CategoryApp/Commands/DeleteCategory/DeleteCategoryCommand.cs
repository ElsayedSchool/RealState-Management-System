using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class DeleteCategoryCommand : IRequestWrapper<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(p => p.Id).GreaterThanOrEqualTo(1).WithMessage("Please send required Category Data");
        }
    }
}
