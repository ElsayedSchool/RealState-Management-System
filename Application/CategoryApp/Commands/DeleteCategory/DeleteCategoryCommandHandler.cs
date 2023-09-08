using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CategoryApp
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IApplicationRepo _repo;

        public DeleteCategoryCommandHandler(IApplicationRepo repo)
        {
            _repo = repo;
        }

        public async Task<RespDto<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _repo.CategoryRepo.DeleteAsync(request.Id);
        }
    }
}
