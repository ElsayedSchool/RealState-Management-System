using Application.Common.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class SignInCommand : IRequestWrapper<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
