using Application.Authentication;
namespace Application.Common.Interfaces
{
    public interface IJWTToken
    {
        string GenerateToken(TokenData tokenData);
    }
}
