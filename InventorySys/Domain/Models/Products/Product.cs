using Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using static Domain.Misc.EnumsData;

namespace Domain.Models.Products
{
    public class Product : FullAuditedEntity, IEntity
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }

        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
        public long ProductCategoryId { get; set; }
        public ProductStatus Status { get; set; }
        public bool IsWeighted { get; set; }
    }

}
