using CMaaS.Backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMaaS.Backend.Models;


namespace CMaaS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContentTypesController(AppDbContext context)
        {
            _context = context;
        }

        //Create Schema(POST: api/contenttypes)
        [HttpPost]
        public async Task<IActionResult> CreateContentType([FromBody] ContentType contentType) 
        {
            //validation
            if (contentType == null)
            {
                return BadRequest("ContentType is required.");
            }
            if (contentType.TenantId ==0)
            {
                return BadRequest("TenantId is required.");
            }
            if (string.IsNullOrWhiteSpace(contentType.Name))
            {
                return BadRequest("Name is required.");
            }

            _context.ContentTypes.Add(contentType);
            await _context.SaveChangesAsync();

            return Ok(contentType);
        }

        //Get All Schemas for a Tenant (GET: api/contenttypes/{tenantId})
        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetContentTypes(int tenantId)
        {
            var contentTypes = await _context.ContentTypes.Where(ct => ct.TenantId == tenantId).ToListAsync();
            return Ok(contentTypes);
        }
    }
}
