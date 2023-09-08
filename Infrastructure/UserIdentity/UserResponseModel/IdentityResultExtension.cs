using Application.Common.Models;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.UserIdentity.UserResponseModel
{
    public static class IdentityResultExtension
    {
        public static RespDto<bool> ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? new RespDto<bool>() { Data = true }
                : new RespDto<bool>() { Data = false, Error = true, Message = string.Concat(result.Errors.Select(e => e.Description)) };
        }

    }
}
