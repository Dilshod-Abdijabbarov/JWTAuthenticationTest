using JWTAuthenticationTest.Models;

namespace JWTAuthenticationTest.Services
{
    public interface ILoginService
    {
        public string Token(UserModelDTO user);
    }
}
