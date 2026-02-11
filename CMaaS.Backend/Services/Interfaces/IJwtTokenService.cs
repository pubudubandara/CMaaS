using CMaaS.Backend.Models;

namespace CMaaS.Backend.Services.Interfaces
{
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for the authenticated user
        /// </summary>
        /// <param name="user">The user to generate token for</param>
        /// <returns>JWT token string</returns>
        string GenerateToken(User user);
    }
}
