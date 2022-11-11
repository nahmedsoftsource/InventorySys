using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Misc.EnumsData;

namespace Application.ViewModels.Products
{
    public class ChangeProductStatusViewModel
    {
        public long Id { get; set; }
        public ProductStatus Status { get; set; }
    }
}
