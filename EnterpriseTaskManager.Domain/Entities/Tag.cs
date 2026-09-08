using EnterpriseTaskManager.Domain.Common;

namespace EnterpriseTaskManager.Domain.Entities
{
    public class Tag : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
    }
}
