using Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductRepository: IAppRepository
    {
        List<ProductListingViewModel> GetProductListing();
        ProductCountByStatusViewModel GetProductCountByStatus();
        Task<string> ChangeProductStatus(ChangeProductStatusViewModel model);
    }
}
