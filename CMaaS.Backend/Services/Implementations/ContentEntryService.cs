using CMaaS.Backend.Data;
using CMaaS.Backend.Dtos;
using CMaaS.Backend.Models;
using CMaaS.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMaaS.Backend.Services.Implementations
{
    public class ContentEntryService : IContentEntryService
    {
        private readonly AppDbContext _context;
        private readonly IUserContextService _userContext;

        public ContentEntryService(AppDbContext context, IUserContextService userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<ServiceResult<ContentEntry>> CreateEntryAsync(ContentEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentException("ContentEntry is required.");
            }

            if (entry.Data == null)
            {
                throw new ArgumentException("Data is required.");
            }

            if (entry.ContentTypeId == 0)
            {
                throw new ArgumentException("ContentTypeId is required.");
            }

            // Verify content type exists
            var contentType = await _context.ContentTypes.FindAsync(entry.ContentTypeId);
            if (contentType == null)
            {
                throw new KeyNotFoundException("Invalid ContentTypeId.");
            }

            // Set tenant ID from current user
            entry.TenantId = _userContext.GetTenantId() ?? 0;

            try
            {
                _context.ContentEntries.Add(entry);
                await _context.SaveChangesAsync();

                return ServiceResult<ContentEntry>.Success(entry);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create entry: {ex.Message}");
            }
        }

        public async Task<ServiceResult<PaginatedResultDto<ContentEntry>>> GetEntriesByTypeAsync(int contentTypeId, FilterDto filter)
        {
            if (contentTypeId <= 0)
            {
                throw new ArgumentException("Invalid ContentTypeId.");
            }

            if (filter.Page <= 0)
            {
                throw new ArgumentException("Page must be greater than 0.");
            }

            if (filter.PageSize <= 0)
            {
                throw new ArgumentException("PageSize must be greater than 0.");
            }

            try
            {
                // Get all entries for the content type from database
                var allEntries = await _context.ContentEntries
                                    .Where(e => e.ContentTypeId == contentTypeId && e.TenantId == _userContext.GetTenantId())
                                    .ToListAsync();

                // Apply search filter in memory (client-side)
                IEnumerable<ContentEntry> filteredEntries = allEntries;

                if (!string.IsNullOrEmpty(filter.SearchTerm))
                {
                    filteredEntries = allEntries.Where(e =>
                        e.Data.RootElement.ToString().Contains(filter.SearchTerm, StringComparison.OrdinalIgnoreCase));
                }

                // Get total count after filtering
                var totalRecords = filteredEntries.Count();

                // Apply pagination
                var entries = filteredEntries
                                    .Skip((filter.Page - 1) * filter.PageSize)
                                    .Take(filter.PageSize)
                                    .ToList();

                var result = new PaginatedResultDto<ContentEntry>
                {
                    TotalRecords = totalRecords,
                    Page = filter.Page,
                    PageSize = filter.PageSize,
                    TotalPages = (int)Math.Ceiling((double)totalRecords / filter.PageSize),
                    Data = entries
                };

                return ServiceResult<PaginatedResultDto<ContentEntry>>.Success(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve entries: {ex.Message}");
            }
        }

        public async Task<ServiceResult<ContentEntry>> GetEntryByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid entry ID.");
            }

            try
            {
                var entry = await _context.ContentEntries
                    .Where(e => e.Id == id && e.TenantId == _userContext.GetTenantId())
                    .FirstOrDefaultAsync();
                
                if (entry == null)
                {
                    throw new KeyNotFoundException("ContentEntry not found.");
                }

                return ServiceResult<ContentEntry>.Success(entry);
            }
            catch (Exception ex) when (ex is not ArgumentException && ex is not KeyNotFoundException)
            {
                throw new Exception($"Failed to retrieve entry: {ex.Message}");
            }
        }

        public async Task<ServiceResult<bool>> DeleteEntryAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid entry ID.");
            }

            try
            {
                var entry = await _context.ContentEntries
                    .Where(e => e.Id == id && e.TenantId == _userContext.GetTenantId())
                    .FirstOrDefaultAsync();
                if (entry == null)
                {
                    throw new KeyNotFoundException("ContentEntry not found.");
                }

                // Get the Tenant ID of the currently logged-in user
                var currentTenantId = _userContext.GetTenantId();
                var userRole = _userContext.GetUserRole();

                // Even if user is Admin, they cannot delete data from other tenants
                if (userRole == "Admin" && entry.TenantId != currentTenantId)
                {
                    throw new UnauthorizedAccessException("You can only delete your own data!");
                }

                _context.ContentEntries.Remove(entry);
                await _context.SaveChangesAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex) when (ex is not ArgumentException && ex is not KeyNotFoundException && ex is not UnauthorizedAccessException)
            {
                throw new Exception($"Failed to delete entry: {ex.Message}");
            }
        }
    }
}
