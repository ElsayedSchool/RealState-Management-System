using Application.CommentApp.Query.GetAllCommentsQuery;
using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommentApp.Query.GetAllOfferCommentsQuery
{
    public class GetAllOfferCommentsQueryHandler : IRequestHandler<GetAllOfferCommentsQuery, List<CommentDetailVm>>
    {
        private readonly IApplicationRepo _repo;

        public GetAllOfferCommentsQueryHandler(IApplicationRepo repo)
        {
            _repo = repo;
        }

        public async Task<RespDto<List<CommentDetailVm>>> Handle(GetAllOfferCommentsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.CommentRepo.GetAllOfferCommentsQuery(request.OfferId);
        }
    }
}
