using Application.Common.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Queries.GetRolesQuery
{
    public class GetUserRolesQuery : IRequestWrapper<IList<string>>
    {
        public string userId { get; set; }
    }
}
