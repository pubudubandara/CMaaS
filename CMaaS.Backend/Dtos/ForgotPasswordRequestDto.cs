namespace CMaaS.Backend.Dtos
{
    /// <summary>
    /// Request DTO for forgot password endpoint
    /// </summary>
    public class ForgotPasswordRequestDto
    {
        /// <summary>
        /// The email address of the user requesting password reset
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
