using Application.Common.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class ChangePasswordCommand : IRequestWrapper<bool>
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public bool isPasswordMatching()
        {
            return NewPassword == ConfirmPassword;
        }
    }
}
