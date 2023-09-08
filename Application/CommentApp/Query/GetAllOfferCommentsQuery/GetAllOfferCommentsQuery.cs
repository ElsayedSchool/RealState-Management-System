using Application.CommentApp.Query.GetAllCommentsQuery;
using Application.Common.CustomValidators;
using Application.Common.Mediatr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommentApp.Query.GetAllOfferCommentsQuery
{
    public class GetAllOfferCommentsQuery : IRequestWrapper<List<CommentDetailVm>>
    {
        public int OfferId { get; set; }
    }

    public class GetAllOfferCommentsQueryValidator : AbstractValidator<GetAllOfferCommentsQuery>
    {
        public GetAllOfferCommentsQueryValidator()
        {
            RuleFor(p => p.OfferId).ValidateIntKey();
        }
    }
}
