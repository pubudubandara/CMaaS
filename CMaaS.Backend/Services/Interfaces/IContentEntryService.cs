using CMaaS.Backend.Dtos;
using CMaaS.Backend.Models;

namespace CMaaS.Backend.Services.Interfaces
{
    public interface IContentEntryService
    {
        /// <summary>
        /// Creates a new content entry
        /// </summary>
        /// <param name="entry">Content entry to create</param>
        /// <returns>Created content entry</returns>
        Task<ServiceResult<ContentEntry>> CreateEntryAsync(ContentEntry entry);

        /// <summary>
        /// Gets all content entries for a specific content type with filtering and pagination
        /// </summary>
        /// <param name="contentTypeId">Content type ID</param>
        /// <param name="filter">Filter and pagination parameters</param>
        /// <returns>Paginated list of content entries</returns>
        Task<ServiceResult<PaginatedResultDto<ContentEntry>>> GetEntriesByTypeAsync(int contentTypeId, FilterDto filter);

        /// <summary>
        /// Gets a single content entry by ID
        /// </summary>
        /// <param name="id">Entry ID</param>
        /// <returns>Content entry if found</returns>
        Task<ServiceResult<ContentEntry>> GetEntryByIdAsync(int id);
    }
}
