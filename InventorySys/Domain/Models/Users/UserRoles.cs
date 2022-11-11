using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class UserRoles : IdentityUserRole<long>
    {


        public virtual User User { get; set; }

        public virtual Role Role { get; set; }



    }
}
