namespace CMaaS.Backend.Dtos
{
    public class VerifyEmailRequestDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
