using Application.Common.Interfaces;
using Application.Common.Mediatr;
using Application.Common.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<AllUsersVm>>
    {
        private readonly IApplicationRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetAllUsersQueryHandler(IApplicationRepo repo,IMapper mapper, ICurrentUserService currentUser)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<RespDto<List<AllUsersVm>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.UserRepo.GetAllAsync();
            if (users == null || users.Error == true || users.Data == null) return new RespDto<List<AllUsersVm>> { Error = true, Message = "حدث خطا اثناء استعاده بيانات المستخدمين برجاء اعاده تحميل الصفحه" };
            var allUsers = users.Data.Where(p => p.Id != _currentUser.UserId).ToList();
            return new RespDto<List<AllUsersVm>>() { Data = _mapper.Map<List<AllUsersVm>>(allUsers)};
        }
    }
}
