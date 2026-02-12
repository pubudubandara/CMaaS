using CMaaS.Backend.Models;
using CMaaS.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Get all tenants
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTenants()
        {
            var result = await _tenantService.GetAllTenantsAsync();

            if (!result.IsSuccess)
            {
                throw new Exception(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // Create a new tenant
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTenant([FromBody] Tenant tenant)
        {
            var result = await _tenantService.CreateTenantAsync(tenant);

            if (!result.IsSuccess)
            {
                throw new ArgumentException(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetAllTenants), new { id = result.Data!.Id }, result.Data);
        }
        // DELETE: api/tenants/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var result = await _tenantService.DeleteTenantAsync(id);

            if (!result.IsSuccess)
            {
                throw new KeyNotFoundException(result.ErrorMessage);
            }

            return NoContent();
        }
    }
}
