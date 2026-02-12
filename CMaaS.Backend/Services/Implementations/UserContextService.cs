using CMaaS.Backend.Services.Interfaces;
using System.Security.Claims;

namespace CMaaS.Backend.Services.Implementations
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetUserId()
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier); // or "id"
            return idClaim != null ? int.Parse(idClaim.Value) : null;
        }

        public int? GetTenantId()
        {
            var tenantClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("TenantId");
            return tenantClaim != null ? int.Parse(tenantClaim.Value) : null;
        }

        public string GetUserRole()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }
    }
}