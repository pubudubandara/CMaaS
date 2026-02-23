using CMaaS.Backend.Dtos;
using CMaaS.Backend.Models;

namespace CMaaS.Backend.Services.Interfaces
{
    public interface IAuthService
    {
        // Registers a new company (tenant) with an admin user
        Task<ServiceResult<RegisterResponseDto>> RegisterCompanyAsync(RegisterRequestDto request);


        // Authenticates a user and generates a JWT token
        Task<ServiceResult<string>> LoginAsync(UserDto request);

        // Verify user email with token
        Task<ServiceResult<string>> VerifyEmailAsync(VerifyEmailRequestDto request);

        // Resend verification email
        Task<ServiceResult<string>> ResendVerificationEmailAsync(ResendVerificationEmailDto request);
    }
}
