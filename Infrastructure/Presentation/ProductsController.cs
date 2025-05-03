using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrorsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    //API Controller 
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {


        //end point : public non-static method


        //sort : nameasc 

        //sort :namedesc

        //sort : priceDesk

        //sort : priceAsk


        [HttpGet] //Get : /api/products
        [ProducesResponseType(StatusCodes.Status200OK , Type = typeof(PaginationResponse<ProductResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters specParams)
        {
            var result = await serviceManager.ProductService.GetAllProductAsync(specParams);
            if (result is null) return BadRequest(); //400
            return Ok(result); //200

        }
        [HttpGet("{id}")] //Get: / api/ products/12
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResultDto>> GetProductsById(int id) //Get : /api /product/12
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound(); //400
            return Ok(result); //200
        }
        [HttpGet("brands")] //Get: / api/ products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandResultDto>> GetAllBrands()
        {
            var result = await serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result); //200
        }


        [HttpGet("types")] //Get: / api/ products/types
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypesResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<TypesResultDto>> GetAllTypes()
        {
            var result = await serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); //400
            return Ok(result); //200
        }




    }
}
