using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Users
{
    public class PermissionGroup : IEntity
    {
        public long Id { get; set; }
        public string GroupNameEn { get; set; }
        public string GroupNameAr { get; set; }
    }
}
