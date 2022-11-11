using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class Permission : IEntity
    {
        public long Id { get; set; }
        public string PermissionKey { get; set; }
        public string PermissionNameEn { get; set; }
        public string PermissionNameAr { get; set; }


        [ForeignKey("PermissionGroupId")]
        public PermissionGroup PermissionGroup { get; set; }
        public long PermissionGroupId { get; set; }
    }
}
