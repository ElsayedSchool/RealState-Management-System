using Application.Common.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.DeactivateUserCommand
{
    public class DeactivateUserCommand : IRequestWrapper<bool>
    {
        public string Id { get; set; }
        public bool Deactivate { get; set; }
    }
}
