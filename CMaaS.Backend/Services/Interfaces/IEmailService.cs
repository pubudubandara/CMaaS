namespace CMaaS.Backend.Services.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Send verification email with token
        /// </summary>
        Task<bool> SendEmailVerificationAsync(string email, string fullName, string verificationToken, string verificationLink);

        /// <summary>
        /// Send password reset email with token
        /// </summary>
        Task<bool> SendPasswordResetEmailAsync(string email, string fullName, string resetToken, string resetLink);

        /// <summary>
        /// Send welcome email after successful registration
        /// </summary>
        Task<bool> SendWelcomeEmailAsync(string email, string fullName);
    }
}
