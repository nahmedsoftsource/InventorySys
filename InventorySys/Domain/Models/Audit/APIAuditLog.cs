using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Audit
{
    public class APIAuditLog : FullAuditedEntity, IEntity
    {
        public string AuditType { get; set; }         /*Type of service -- Clients  --Update Client--/*/
        public string Request { get; set; }            /*Table where rows been created/updated/deleted*/
        public string Response { get; set; }            /*Table where rows been created/updated/deleted*/
    }
}
