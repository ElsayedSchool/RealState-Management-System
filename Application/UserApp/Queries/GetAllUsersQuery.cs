using Application.Common.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.Queries
{
    public class GetAllUsersQuery : IRequestWrapper<List<AllUsersVm>>
    {
    }
}
