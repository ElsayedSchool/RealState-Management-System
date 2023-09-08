
using Application.Authentication;

namespace Infrastructure.InfrastructureService.JWTToken.CreateToken
{
    public interface ICreateToken
    {
        string execute(TokenData tokenData);
    }
}