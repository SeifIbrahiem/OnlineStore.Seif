using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet] //Get: /api/baskets?id=sadas
        public async Task <IActionResult> GetBasketById(string id)
        {
            var result = await serviceManager.BasketServices.GetBasketAsync(id);
            return Ok(result);
        }

        [HttpPost] // post : /api/baskets
        public async Task <IActionResult> UpdateBasket(BasketDto basketDto)
        {
            var result = await serviceManager.BasketServices.UpdateBasketAsync(basketDto);
            return Ok(result);
        }
   
        [HttpDelete] // Delete : /api/baskets?id
        public async Task <IActionResult> DeleteBasket (string id)
        {
            await serviceManager.BasketServices.DeleteBasketAsync(id);
            return NoContent(); //204
        }
    }
}
