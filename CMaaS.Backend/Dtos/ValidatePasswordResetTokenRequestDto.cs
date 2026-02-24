namespace CMaaS.Backend.Dtos
{
    /// <summary>
    /// Request DTO to validate a password reset token
    /// </summary>
    public class ValidatePasswordResetTokenRequestDto
    {
        /// <summary>
        /// The email address of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The password reset token to validate
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
