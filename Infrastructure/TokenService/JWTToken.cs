using Application.Authentication;
using Application.Common.Interfaces;
using Infrastructure.InfrastructureService.JWTToken.CreateToken;

namespace Infrastructure.InfrastructureService.JWTToken
{
    public class JWTToken: IJWTToken
    {
        private readonly ICreateToken _createToken;

        public JWTToken(ICreateToken createToken)
        {
            _createToken = createToken;
        }

        public string GenerateToken(TokenData tokenData) 
        {
            return _createToken.execute(tokenData);
        }
    }
}
