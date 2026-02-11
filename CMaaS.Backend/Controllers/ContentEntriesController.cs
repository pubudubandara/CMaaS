using CMaaS.Backend.Dtos;
using CMaaS.Backend.Models;
using CMaaS.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMaaS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentEntriesController : ControllerBase
    {
        private readonly IContentEntryService _contentEntryService;

        public ContentEntriesController(IContentEntryService contentEntryService)
        {
            _contentEntryService = contentEntryService;
        }

        /// <summary>
        /// Create a new content entry
        /// </summary>
        /// <param name="entry">Content entry to create</param>
        /// <returns>Created content entry</returns>
        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] ContentEntry entry)
        {
            var result = await _contentEntryService.CreateEntryAsync(entry);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetEntryById), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Get all content entries for a specific content type with filtering and pagination
        /// </summary>
        /// <param name="contentTypeId">Content type ID</param>
        /// <param name="filter">Filter and pagination parameters</param>
        /// <returns>Paginated list of content entries</returns>
        [HttpGet("{contentTypeId}")]
        public async Task<IActionResult> GetEntriesByType(int contentTypeId, [FromQuery] FilterDto filter)
        {
            var result = await _contentEntryService.GetEntriesByTypeAsync(contentTypeId, filter);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Get a single content entry by ID
        /// </summary>
        /// <param name="id">Entry ID</param>
        /// <returns>Content entry</returns>
        [HttpGet("entry/{id}")]
        public async Task<IActionResult> GetEntryById(int id)
        {
            var result = await _contentEntryService.GetEntryByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
