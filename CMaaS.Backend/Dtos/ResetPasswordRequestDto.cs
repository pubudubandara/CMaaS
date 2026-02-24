namespace CMaaS.Backend.Dtos
{
    /// <summary>
    /// Request DTO for resetting password with token
    /// </summary>
    public class ResetPasswordRequestDto
    {
        /// <summary>
        /// The email address of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The password reset token received via email
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The new password to set
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Confirmation of the new password
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
