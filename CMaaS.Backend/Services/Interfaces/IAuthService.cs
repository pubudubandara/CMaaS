using CMaaS.Backend.Dtos;
using CMaaS.Backend.Models;

namespace CMaaS.Backend.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new company (tenant) with an admin user
        /// </summary>
        /// <param name="request">Registration details including organization and admin info</param>
        /// <returns>Registration result with tenant ID and API key</returns>
        Task<ServiceResult<RegisterResponseDto>> RegisterCompanyAsync(RegisterRequestDto request);

        /// <summary>
        /// Authenticates a user and generates a JWT token
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>JWT token if authentication is successful</returns>
        Task<ServiceResult<string>> LoginAsync(UserDto request);
    }
}
