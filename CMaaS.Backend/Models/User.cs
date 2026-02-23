using System.Text.Json.Serialization;

namespace CMaaS.Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int TenantId { get; set; }

        public UserRole Role { get; set; } = UserRole.User;

        // Email Verification Fields
        public bool IsEmailVerified { get; set; } = false;
        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiry { get; set; }

        // Password Reset Fields
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }

        // when converting to JSON, ignore the Tenant property to avoid circular reference issues and reduce payload size
        [JsonIgnore]
        public Tenant? Tenant { get; set; }
    }
}
