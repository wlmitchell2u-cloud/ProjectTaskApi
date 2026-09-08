
using EnterpriseTaskManager.Application.DTOs.Comments;
namespace EnterpriseTaskManager.Application.Services.Interfaces;

public interface ITaskCommentService
{
    Task<List<TaskCommentDto>> GetAllByTaskAsync(int taskId, CancellationToken cancellationToken);
    Task<TaskCommentDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<TaskCommentDto> CreateAsync(CreateTaskCommentDto dto, CancellationToken cancellationToken);
    Task<TaskCommentDto?> UpdateAsync(int id, CreateTaskCommentDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}