using Application.Interfaces;
using Application.ViewModels.Products;
using Dapper;
using Data.Context;
using Domain.Interfaces;
using Domain.Models.Products;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBConnectionFactory _connectionFactory;
        private readonly ILogger _logger;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<ProductCategory> _productCategoryRepo;

        public ProductRepository(
            DBConnectionFactory connectionFactory,
            ILoggerFactory logger,
            IRepository<Product> productRepo,
            IRepository<ProductCategory> productCategoryRepo
            )
        {
            this._connectionFactory = connectionFactory;
            this._logger = logger.CreateLogger<ProductRepository>();
            this._productRepo = productRepo;
            this._productCategoryRepo= productCategoryRepo;
        }

        public async Task<string> ChangeProductStatus(ChangeProductStatusViewModel model)
        {
            string response = "error";
            try
            {
                var data = _productRepo.GetById(model.Id).Result;

                if(data != null && data.Id > 0)
                {
                    data.Status = model.Status;

                    await _productRepo.Update(model.Id, data);
                    response= "success";
                }

            }catch (Exception ex)
            {
                _logger.LogError("ChangeProductStatus: Could not update record. Message:{0}. Exception: {1}", ex.Message, ex.InnerException);
            }
            return response;
        }

        public ProductCountByStatusViewModel GetProductCountByStatus()
        {
            using IDbConnection conn = _connectionFactory.GetOpenConnection();

            return conn.Query<ProductCountByStatusViewModel>("GetProductCountByStatus", commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public List<ProductListingViewModel> GetProductListing()
        {
            throw new NotImplementedException();
        }
    }
}
