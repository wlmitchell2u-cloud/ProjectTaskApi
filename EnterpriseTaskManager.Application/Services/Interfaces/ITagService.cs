using EnterpriseTaskManager.Application.DTOs.Tags;

namespace EnterpriseTaskManager.Application.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<TagDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<TagDto> CreateAsync(CreateTagDto dto, CancellationToken cancellationToken);
        Task<TagDto?> UpdateAsync(int id, CreateTagDto dto, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
