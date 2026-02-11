using CMaaS.Backend.Models;
using CMaaS.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMaaS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        /// <summary>
        /// Get all tenants
        /// </summary>
        /// <returns>List of all tenants</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            var result = await _tenantService.GetAllTenantsAsync();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Create a new tenant
        /// </summary>
        /// <param name="tenant">Tenant to create</param>
        /// <returns>Created tenant</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] Tenant tenant)
        {
            var result = await _tenantService.CreateTenantAsync(tenant);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetAllTenants), new { id = result.Data!.Id }, result.Data);
        }
    }
}
