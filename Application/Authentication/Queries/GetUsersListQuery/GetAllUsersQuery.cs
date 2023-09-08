using Application.Common.Mediatr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Queries.GetUsersListQuery
{
    public class GetAllUsersQuery: IRequestWrapper<List<UsersListVm>>
    {
    }
}
