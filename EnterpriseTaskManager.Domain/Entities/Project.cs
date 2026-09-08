using EnterpriseTaskManager.Domain.Common;
using EnterpriseTaskManager.Domain.Enums;

namespace EnterpriseTaskManager.Domain.Entities
{
    public class Project : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string ProjectNumber { get; set; } = string.Empty;
        public string? Description {  get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.Active;

        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
