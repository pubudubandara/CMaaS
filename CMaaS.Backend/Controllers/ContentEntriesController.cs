using CMaaS.Backend.Data;
using CMaaS.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace CMaaS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentEntriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContentEntriesController(AppDbContext context)
        {
            _context = context;
        }

        //Create Content Entry (POST: api/contententries)
        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] ContentEntry entry)
        {
            //validation
            if (entry == null)
            {
                return BadRequest("ContentEntry is required.");
            }

            if (entry.Data == null)
            {
                return BadRequest("Data is required.");
            }

            var contentType = await _context.ContentTypes.FindAsync(entry.ContentTypeId);
            if (contentType == null)
            {
                return BadRequest("Invalid ContentTypeId.");
            }
            _context.ContentEntries.Add(entry);
            await _context.SaveChangesAsync();
            return Ok(entry);
        }

        //Get Entries by Type (GET: api/contententries/{contentTypeId})
        [HttpGet("{contentTypeId}")]
        public async Task<IActionResult> GetEntriesByType(int contentTypeId)
        {
            var entries = await _context.ContentEntries
                                        .Where(e => e.ContentTypeId == contentTypeId)
                                        .ToListAsync();
            return Ok(entries);
        }

        // 3. Get Single Entry (GET: api/contententries/entry/{id})
        [HttpGet("entry/{id}")]
        public async Task<IActionResult> GetEntryById(int id)
        {
            var entry = await _context.ContentEntries.FindAsync(id);
            if (entry == null) return NotFound();
            return Ok(entry);
        }
    }
}
