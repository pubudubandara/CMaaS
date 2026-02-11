using CMaaS.Backend.Dtos;
using CMaaS.Backend.Models;

namespace CMaaS.Backend.Services.Interfaces
{
    public interface ITenantService
    {
        /// <summary>
        /// Gets all tenants
        /// </summary>
        /// <returns>List of all tenants</returns>
        Task<ServiceResult<List<Tenant>>> GetAllTenantsAsync();

        /// <summary>
        /// Creates a new tenant
        /// </summary>
        /// <param name="tenant">Tenant to create</param>
        /// <returns>Created tenant</returns>
        Task<ServiceResult<Tenant>> CreateTenantAsync(Tenant tenant);
    }
}
