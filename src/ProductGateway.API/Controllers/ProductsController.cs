using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ProductGateway.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public ProductsController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<GetProductResponse>> Get([FromRoute]int productId)
        {
            var productDetail = GetProductDetailAsync(productId);
            var productPrice = GetProductPriceAsync(productId);

            await Task.WhenAll(productDetail, productPrice);

            return Ok(new GetProductResponse
            {
                    ProductId = productDetail.Result.ProductId,
                    Name = productDetail.Result.Name,
                    Description = productDetail.Result.Description,
                    Price = productPrice.Result.Price
            });
        }

        private async Task<GetProductDetailResponse> GetProductDetailAsync(int productId)
        {
            GetProductDetailResponse productDetailResponse = null;

            HttpClient client = _clientFactory.CreateClient();

            string productApiBaseUrl = _configuration.GetValue<string>("Product_API_Host");

            HttpResponseMessage response = await client.GetAsync(requestUri: $"{productApiBaseUrl}/api/products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                productDetailResponse = JsonConvert.DeserializeObject<GetProductDetailResponse>(await response.Content.ReadAsStringAsync());
            }

            return productDetailResponse;
        }

        private async Task<GetPriceResponse> GetProductPriceAsync(int productId)
        {
            GetPriceResponse productPriceResponse = null;

            HttpClient client = _clientFactory.CreateClient();

            string priceApiBaseUrl = _configuration.GetValue<string>("Price_API_Host");

            HttpResponseMessage response = await client.GetAsync(requestUri: $"{priceApiBaseUrl}/api/prices?productId={productId}");

            if (response.IsSuccessStatusCode)
            {
                productPriceResponse = JsonConvert.DeserializeObject<GetPriceResponse>(await response.Content.ReadAsStringAsync());
            }

            return productPriceResponse;
        }
    }

    public class GetProductResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }

    public class GetProductDetailResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class GetPriceResponse
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
    }
}
