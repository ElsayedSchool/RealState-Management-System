using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authentication{
    public class JWTSecurityToken
    {
        public String TokenData { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
