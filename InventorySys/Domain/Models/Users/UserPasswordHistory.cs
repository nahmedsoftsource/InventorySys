using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class UserPasswordHistory : FullAuditedEntity, IEntity
    {
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public string PasswordHash { get; set; }
    }

}
