using CMaaS.Backend.Dtos;
using CMaaS.Backend.Models;

namespace CMaaS.Backend.Services.Interfaces
{
    public interface IContentTypeService
    {
        /// <summary>
        /// Creates a new content type (schema)
        /// </summary>
        /// <param name="contentType">Content type to create</param>
        /// <returns>Created content type</returns>
        Task<ServiceResult<ContentType>> CreateContentTypeAsync(ContentType contentType);

        /// <summary>
        /// Gets all content types for a specific tenant
        /// </summary>
        /// <param name="tenantId">Tenant ID</param>
        /// <returns>List of content types</returns>
        Task<ServiceResult<List<ContentType>>> GetContentTypesByTenantAsync(int tenantId);
    }
}
