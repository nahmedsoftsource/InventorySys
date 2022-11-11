using Domain.Models.Audit;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using static Domain.Misc.EnumsData;

namespace Domain.Misc
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public AuditType AuditType { get; set; }
        public long AuditUser { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public List<string> ChangedColumns { get; } = new List<string>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public AuditLog ToAudit()
        {
            var audit = new AuditLog();
            audit.TableName = TableName;
            audit.AuditType = AuditType.ToString();
            audit.AuditUser = AuditUser;
            audit.AuditDateTime = DateTime.Now;
            audit.KeyValues = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.ChangedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
