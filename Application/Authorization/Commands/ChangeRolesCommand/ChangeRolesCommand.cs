using Application.Common.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authorization.Commands.ChangeRolesCommand
{
    public class ChangeRolesCommand : IRequestWrapper<bool>
    {
        public string Id { get; set; }
        public List<string> Roles { get; set; }
    }
}
