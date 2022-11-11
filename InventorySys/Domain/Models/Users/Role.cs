using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class Role : IdentityRole<long>, IEntity<long>
    {
        public bool IsDeletable { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
