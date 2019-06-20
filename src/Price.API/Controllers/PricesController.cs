using Microsoft.AspNetCore.Mvc;

namespace Price.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<GetPriceResponse> Get(int productId)
        {
            return Ok(new GetPriceResponse { ProductId = productId, Price = 100 });
        }
    }

    public class GetPriceResponse
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
    }
}
