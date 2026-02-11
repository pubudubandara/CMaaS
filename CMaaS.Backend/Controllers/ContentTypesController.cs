using CMaaS.Backend.Models;
using CMaaS.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMaaS.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTypesController : ControllerBase
    {
        private readonly IContentTypeService _contentTypeService;

        public ContentTypesController(IContentTypeService contentTypeService)
        {
            _contentTypeService = contentTypeService;
        }

        /// <summary>
        /// Create a new content type (schema)
        /// </summary>
        /// <param name="contentType">Content type to create</param>
        /// <returns>Created content type</returns>
        [HttpPost]
        public async Task<IActionResult> CreateContentType([FromBody] ContentType contentType)
        {
            var result = await _contentTypeService.CreateContentTypeAsync(contentType);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetContentTypes), new { tenantId = result.Data!.TenantId }, result.Data);
        }

        /// <summary>
        /// Get all content types (schemas) for a specific tenant
        /// </summary>
        /// <param name="tenantId">Tenant ID</param>
        /// <returns>List of content types</returns>
        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetContentTypes(int tenantId)
        {
            var result = await _contentTypeService.GetContentTypesByTenantAsync(tenantId);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
