using Application.Common.CustomValidators;
using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommentApp.Command.DeleteComment
{
    public class DeleteCommentCommand : IRequestWrapper<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(p => p.Id).ValidateIntKey();
        }
    }
}
