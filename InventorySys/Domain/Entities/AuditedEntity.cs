using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class AuditedEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual long? CreatedBy { get; set; }
        public virtual DateTime? LastModifiedOn { get; set; }
        public virtual long? LastModifiedBy { get; set; }

    }
    public abstract class AuditiedEntity : AuditedEntity<long>
    {

    }
}
