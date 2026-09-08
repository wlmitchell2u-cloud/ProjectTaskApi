using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace EnterpriseTaskManager.Domain.Common
{
    public abstract class AuditableEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime? UpdatedAtUtc { get; set; }

        public string? UpdatedBy {  get; set; }
    }
}
