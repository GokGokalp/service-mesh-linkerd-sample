using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{productId}")]
        public ActionResult<GetProductDetailResponse> Get([FromRoute]int productId)
        {
            return Ok(new GetProductDetailResponse { ProductId = productId, Name = "Macbook Pro", Description = "Intel Core i5 8GB 256GB SSD MacOs Sierra" });
        }
    }

    public class GetProductDetailResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}