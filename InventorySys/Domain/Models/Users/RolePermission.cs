using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class RolePermission : AuditiedEntity, IEntity<long>
    {
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public long RoleId { get; set; }

        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }

        public long PermissionId { get; set; }

    }
}
