using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using AutoMapper;

namespace Application.OfferApp.Queries.GetOfferDetail
{
    public class GetOfferDetailQueryHandler : IRequestHandler<GetOfferDetailQuery, OfferDetailVm>
    {
        private readonly IApplicationRepo _repo;
        private readonly IMapper _mapper;

        public GetOfferDetailQueryHandler(IApplicationRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RespDto<OfferDetailVm>> Handle(GetOfferDetailQuery request, CancellationToken cancellationToken)
        {
            var resp =  await _repo.OfferRepo.GetOfferDetail(request.OfferId);
            if (resp == null || resp.Error == true) return new RespDto<OfferDetailVm>() { Error = true, Message = "حدث خطا اثناء استرجاع البيانات برجاء اعاده تحميل الصفحه"};
            return resp;
        }
    }
}
