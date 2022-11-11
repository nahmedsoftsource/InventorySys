using Application.Interfaces;
using Application.ViewModels.General;
using Application.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Misc.EnumsData;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IProductRepository _productRepository;
        public ProductController(
            ILoggerFactory loggerFactory,
            IProductRepository productRepository)
        {
            this._logger = loggerFactory.CreateLogger<ProductController>();
            this._productRepository = productRepository;

        }

    
        [HttpGet, Route("GetProductCountByStatus")]
        public IActionResult GetProductCount()
        {
            var apiResult = new APIResult { IsSuccess = false, ResponseCode = ResponseCodes.NO_RECORD };

            var data = _productRepository.GetProductCountByStatus();
            if (data != null )
            {
                apiResult.IsSuccess = true;
                apiResult.ResponseCode = ResponseCodes.SUCCESS;
                apiResult.Data = data;
            }

            return Ok(apiResult);
        }


        [HttpPost, Route("ChangeProductStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeProductStatusViewModel model)
        {
            var apiResult = new APIResult{IsSuccess = false };
            if(model.Status  > 0 && model.Id > 0)
            {
                var response  = await _productRepository.ChangeProductStatus(model);
                if (response  == "success")
                {
                    apiResult.IsSuccess = true;
                    apiResult.ResponseCode = ResponseCodes.SUCCESS;

                }
                else
                {
                    apiResult.ResponseCode = ResponseCodes.NO_RECORD;
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.ResponseCode = ResponseCodes.BAD_REQUEST;
            }
           

            return Ok(apiResult);
        }


        [HttpPost, Route("SellProduct")]
        public async Task<IActionResult> SellProductById(long Id)
        {
            var apiResult = new APIResult { IsSuccess = false };
            if (Id > 0)
            {
                ChangeProductStatusViewModel model = new ChangeProductStatusViewModel
                {
                    Id = Id,
                    Status = ProductStatus.SOLD
                };

                var response = await _productRepository.ChangeProductStatus(model);
                if (response == "success")
                {
                    apiResult.IsSuccess = true;
                    apiResult.ResponseCode = ResponseCodes.SUCCESS;

                }
                else
                {
                    apiResult.ResponseCode = ResponseCodes.NO_RECORD;
                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.ResponseCode = ResponseCodes.BAD_REQUEST;
            }


            return Ok(apiResult);
        }






    }
}
