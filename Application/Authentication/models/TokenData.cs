using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Authentication{
    public class TokenData
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public IList<string> Roles { get; set; }
        public string SecurityKey { get; set; }

        public TokenData GetTokenData(ApplicationUser user, List<string> role, string securityKey)
        {
            return new TokenData() { Name = user.UserName, Roles = role, UserId = user.Id, SecurityKey = securityKey };
        }
    }
}
