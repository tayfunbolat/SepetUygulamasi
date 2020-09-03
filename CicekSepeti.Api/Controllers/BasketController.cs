using CicekSepeti.Domain;
using CicekSepeti.Domain.Models;
using CicekSepeti.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CicekSepeti.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {

        private readonly ILogger<BasketController> _logger;
        private readonly IBasketService _basketService;
        public BasketController(ILogger<BasketController> logger
            , IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<ActionResult<List<IBaseEntity>>> AddBasket([FromBody]ProductBasket models)
        {
            var model = await _basketService.AddBasketProduct(models);

            if (model != null)
                return Ok(model);
            else
                return NotFound();
        }

    }
}
