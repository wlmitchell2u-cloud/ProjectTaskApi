using EnterpriseTaskManager.Domain.Common;

namespace EnterpriseTaskManager.Domain.Entities
{
    public class TaskComment : AuditableEntity
    {
        public string Message { get; set; } = string.Empty;
        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;

        public int AuthorId { get; set; }
        public User Author { get; set; } = null!;
    }
}
