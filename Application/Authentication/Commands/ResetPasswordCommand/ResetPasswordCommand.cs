using Application.Common.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.ResetPasswordCommand
{
    public class ResetPasswordCommand:IRequestWrapper<bool>
    {
        public string Id { get; set; }
    }
}
