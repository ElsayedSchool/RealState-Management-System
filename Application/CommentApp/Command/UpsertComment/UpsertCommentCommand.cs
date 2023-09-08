using Application.Common.CustomValidators;
using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommentApp.Command.UpsertComment
{
    public class UpsertCommentCommand: IRequestWrapper<bool>
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public string Details { get; set; }
    }

    public class UpsertCommentCommandValidator : AbstractValidator<UpsertCommentCommand>
    {
        public UpsertCommentCommandValidator()
        {
            RuleFor(p => p.OfferId).GreaterThanOrEqualTo(1).WithMessage("من فضلك ارسل البيانات صحيحه");
            
            RuleFor(p => p.Details).NotEmpty().WithMessage("الملاحظات المرسله لا تحتوى على بيانات");
        }
    }
}
