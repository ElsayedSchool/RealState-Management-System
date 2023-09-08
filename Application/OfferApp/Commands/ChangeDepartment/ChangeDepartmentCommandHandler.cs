using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OfferApp.Commands.ChangeDepartment
{
    public class ChangeDepartmentCommandHandler : IRequestHandler<ChangeDepartmentCommand, bool>
    {
        private readonly IApplicationRepo _repo;

        public ChangeDepartmentCommandHandler(IApplicationRepo repo)
        {
            _repo = repo;
        }

        public async Task<RespDto<bool>> Handle(ChangeDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await _repo.OfferRepo.UpdateOffersDepartment(request);
        }
    }
}
