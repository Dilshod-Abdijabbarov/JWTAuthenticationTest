using JWTAuthenticationTest.Data;
using JWTAuthenticationTest.Models;

namespace JWTAuthenticationTest.Services
{
    public class UserService
    {
        private readonly AuthenticationTest _authenticationTest;

        public UserService(AuthenticationTest authenticationTest)
        {
            _authenticationTest = authenticationTest;
        }

        public async Task<bool> Create(User user)
        {
            await _authenticationTest.RoleUser.AddAsync(new UserRole() { RoleId = 111, UserId = user.Id });
            //var u = await _authenticationTest.Users.FirstOrDefaultAsync();
            if (await _authenticationTest.SaveChangesAsync() <= 0)
                return false;

            return true;
        }     
    }
}
