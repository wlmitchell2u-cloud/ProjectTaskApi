using EnterpriseTaskManager.Domain.Enums;

namespace EnterpriseTaskManager.Application.DTOs.Tasks;

public class CreateTaskItemDto
{
    public string Title { get; set; } = string.Empty;

    public string TaskNumber { get; set; } = string.Empty;
    public string? Description {  get; set; }

    public TaskItemStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDateUtc { get; set; }
    public decimal? EstimatedHours { get; set; }
    public DateTime? CompletedDateUtc { get; set; }
    public int ProjectId { get; set; }
    public int? AssignedToUserId { get; set; }
}
