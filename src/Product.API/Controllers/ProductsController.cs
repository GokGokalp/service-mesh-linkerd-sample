using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{productId}")]
        public ActionResult<GetProductDetailResponse> Get([FromRoute]int productId)
        {
            if (productId == 1)
            {
                return Ok(new GetProductDetailResponse { ProductId = productId, Name = "Macbook Pro", Description = "Intel Core i5 8GB 256GB SSD MacOs Sierra" });
            }
            else
            {
                return NotFound();
            }
        }
    }

    public class GetProductDetailResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}