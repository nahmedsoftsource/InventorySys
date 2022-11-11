using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Audit
{
    public class AuditLog : FullAuditedEntity, IEntity
    {
        public new Guid Id { get; set; }                /*Log Id*/
        public DateTime AuditDateTime { get; set; }  /*Log time*/
        public string AuditType { get; set; }           /*Create, Update or Delete*/
        public long AuditUser { get; set; }            /*Log User*/
        public string TableName { get; set; }           /*Table where rows been created/updated/deleted*/
        public string KeyValues { get; set; }           /*Pk and it's values*/
        public string OldValues { get; set; }           /*Changed column name and old value*/
        public string NewValues { get; set; }           /*Changed column name and current value*/
        public string ChangedColumns { get; set; }      /*Changed column names*/
    }
}
