using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace Price.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<GetPriceResponse> Get([FromQuery]int productId)
        {
            if (productId == 1)
            {
                Thread.Sleep(TimeSpan.FromSeconds(10).Milliseconds);

                return Ok(new GetPriceResponse { ProductId = productId, Price = 100 });
            }
            else
            {
                return NotFound();
            }
        }
    }

    public class GetPriceResponse
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
    }
}
