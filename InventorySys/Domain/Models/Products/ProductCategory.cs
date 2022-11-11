using Domain.Entities;

namespace Domain.Models.Products
{
    public class ProductCategory : FullAuditedEntity, IEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

    }

}
