using EnterpriseTaskManager.Domain.Common;
using EnterpriseTaskManager.Domain.Enums;

namespace EnterpriseTaskManager.Domain.Entities
{
    public class TaskItem : AuditableEntity
    {
        public string Title { get; set; } = string.Empty;

        public string TaskNumber { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.New;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateTime DueDateUtc { get; set; }

        public decimal? EstimatedHours { get; set; }
        
        public DateTime? CompletedDateUtc { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
        public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
    }
}
