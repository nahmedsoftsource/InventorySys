using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class FullAuditedEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, ISoftDelete, IEntity<TPrimaryKey>
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public long? DeletedBy { get; set; }
    }

    public abstract class FullAuditedEntity : FullAuditedEntity<long>
    {
        public FullAuditedEntity()
        {

        }
    }

}
