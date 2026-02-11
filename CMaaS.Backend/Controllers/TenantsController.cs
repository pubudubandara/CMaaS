using CMaaS.Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMaaS.Backend.Models;

namespace CMaaS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor to inject the AppDbContext
        public TenantsController(AppDbContext context)
        {
            _context = context;
        }

        //Get All Tenants (GET: api/tenants)
        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            // Fetch all tenants from the database
            var tenants = await _context.Tenants.ToListAsync();
            return Ok(tenants);
        }

        //Create a new tenant (POST: api/tenants)
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] Tenant tenant)
        {   
            //validation
            if(string.IsNullOrEmpty(tenant.Name))
            {
                return BadRequest("Tenant name is required.");
            }

            //save tenant to database
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllTenants), new { id = tenant.Id }, tenant);
        }

    }
}
