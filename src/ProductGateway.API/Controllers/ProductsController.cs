using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProductGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        [HttpGet]
        public async Task<ActionResult<GetProductResponse>> Get(int productId)
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

            HttpResponseMessage response = await client.GetAsync(requestUri: $"http://localhost:9090/api/products/{productId}");

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

            HttpResponseMessage response = await client.GetAsync(requestUri: $"http://localhost:8080/api/prices?productId={productId}");

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
