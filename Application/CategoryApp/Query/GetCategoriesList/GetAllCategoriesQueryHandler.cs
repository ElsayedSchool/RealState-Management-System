using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoriesListVm>>
    {
        private readonly IApplicationRepo _repo;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IApplicationRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RespDto<List<CategoriesListVm>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            //FindAllAsync(p => p.CategoryParentId != null, new string[1] {"SubCategories"})
            var categories = await _repo.CategoryRepo.GetAllAsync();
            return _mapper.Map<RespDto<List<CategoriesListVm>>>(categories);
        }
    }
}
