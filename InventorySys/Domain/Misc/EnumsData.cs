using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Misc
{
    public class EnumsData
    {
        public enum AuditType
        {
            None = 0,
            Create = 1,
            Update = 2,
            Delete = 3
        }

        public enum ProductStatus
        {
            [Display(Name = "Sold")]
            SOLD = 1,
            [Display(Name = "InStock")]
            IN_STOCK = 2,
            [Display(Name = "Damaged")]
            DAMAGED = 3
        }

        public enum ResponseCodes
        {
            SUCCESS = 200,
            BAD_REQUEST = 400,
            UNAUTHORIZED = 401,
            NO_ACCESS = 402,
            INTERNAL_SERVER_ERROR = 500,
            GENERAL_ERROR = 1000,
            NO_RECORD = 1001,
        }
    }
}
