
using EnterpriseTaskManager.Application.DTOs.Tasks;

namespace EnterpriseTaskManager.Application.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskItemDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TaskItemDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto, CancellationToken cancellationToken);

    Task<TaskItemDto?> UpdateAsync(int id, CreateTaskItemDto dto, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken );
}
