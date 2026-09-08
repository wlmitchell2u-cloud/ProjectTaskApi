using EnterpriseTaskManager.Domain.Common;

namespace EnterpriseTaskManager.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public string PasswordHash {  get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();

    }
}
