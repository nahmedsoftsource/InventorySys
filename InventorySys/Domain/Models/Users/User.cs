using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class User : IdentityUser<long>, IEntity<long>
    {
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public long? DeletedBy { get; set; }
       


        public virtual ICollection<UserRoles> UserRoles { get; set; }
        public virtual ICollection<Role> Roles { get; set; }



    }
}
