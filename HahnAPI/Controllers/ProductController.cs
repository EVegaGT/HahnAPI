using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Requests;
using Domain.Models.Responses.Product;
using Domain.Services.Product.Command;
using Domain.Services.Product.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace HahnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <returns>The Id from the newly product </returns>
        /// <response code="201">Returns the product Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpPost(Name = "CreateProduct")]
        [SwaggerOperation(Summary = "Create a product", Description = "Create a new product")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Product([FromBody] ProductRequest Product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                var command = _mapper.Map<CreateProductCommand>(Product); 
                var result = await _mediator.Send(command);
                return Created("api/product", result);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a product by id.
        /// </summary>
        /// <param name="productId">product Id</param>
        /// <returns>The data from a product by id </returns>
        /// <response code="200">Returns the product</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response>   
        [HttpGet("{productId}", Name = "GetProductById")]
        [SwaggerOperation(Summary = "Get product data", Description = "Request data from a product by id")]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Product(Guid productId)
        {
            try
            {
                var result = await _mediator.Send(new GetProductByIdQuery(productId));
                return Ok(result);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a products by order.
        /// </summary>
        /// <param name="orderId">product Id</param>
        /// <returns>The data from a product by id </returns>
        /// <response code="200">Returns the product</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response>   
        [HttpGet("Order/{orderId}", Name = "GetProductByOrderId")]
        [SwaggerOperation(Summary = "Get products data", Description = "Request data from a products by order id")]
        [ProducesResponseType(typeof(List<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> ProductsByOrder(Guid orderId)
        {
            try
            {
                var result = await _mediator.Send(new GetProductsByOrderIdQuery(orderId));
                return Ok(result);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Products.
        /// </summary>
        /// <returns>The data from a list of products</returns>
        /// <response code="200">Returns the product</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response>   
        [HttpGet(Name = "Products")]
        [SwaggerOperation(Summary = "Get products data", Description = "Request data from products")]
        [ProducesResponseType(typeof(List<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Products()
        {
            try
            {
                var result = await _mediator.Send(new GetProductsQuery());
                return Ok(result);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <returns>The Id from the updated product </returns>
        /// <response code="200">Returns the product Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpPut("{productId}", Name = "UpdateProduct")]
        [SwaggerOperation(Summary = "Update a product", Description = "Update a product by id")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid productId, [FromBody] ProductRequest product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                var command = _mapper.Map<UpdateProductCommand>(product);
                command.ProductId = productId;
                var result = await _mediator.Send(command);
                
                return Ok(result);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <returns>The Id from the deleted product </returns>
        /// <response code="200">Returns the product Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpDelete("{productId}", Name = "DeletedProduct")]
        [SwaggerOperation(Summary = "Delete a product", Description = "Delete a product by id")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid productId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteProductCommand(productId));
                return Ok(result);
            }
            catch (HahnApiException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
