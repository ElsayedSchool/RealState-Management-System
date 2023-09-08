using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Queries.GetOffersList
{
    public class GetAllOffersQueryHandler : IRequestHandler<GetAllOffersQuery, OffersQueryResponse>
    {
        private readonly IApplicationRepo _repo;
        private readonly IMapper _mapper;

        public GetAllOffersQueryHandler(IApplicationRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RespDto<OffersQueryResponse>> Handle(GetAllOffersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.OfferRepo.GetOffersByQuery(request);
        }
    }
}
