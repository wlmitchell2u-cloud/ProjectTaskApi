using EnterpriseTaskManager.Domain.Common;

namespace EnterpriseTaskManager.Domain.Entities
{
    public class ActivityLog : AuditableEntity
    {
        public string EntityType { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string Action { get; set; } = string.Empty;

        public string Actor {  get; set; } = string.Empty;

        public string? MetadataJson { get; set; }

    }
}
