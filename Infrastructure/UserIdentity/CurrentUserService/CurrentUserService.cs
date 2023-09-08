using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Application.Common.Interfaces;

namespace AcademyProject.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
            IsAuthenticated = UserId != null;
        }

        public string GetUserId()
        {
            return UserId;  
        }

        public string UserId { get; }

        public bool IsAuthenticated { get; }
    }
}
