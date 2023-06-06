using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Helpers;
using Domain.Models.Requests.Brand;
using Domain.Models.Responses.Brand;
using Domain.Services.Brand.Commands;
using Domain.Services.Brand.Commands.Handlers;
using Domain.Services.Brand.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace HahnAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BrandController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new brand.
        /// </summary>
        /// <returns>The Id from the newly brand </returns>
        /// <response code="201">Returns the brand Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpPost(Name = "CreateBrand")]
        [SwaggerOperation(Summary = "Create a brand", Description = "Create a new brand")]
        [ProducesResponseType(typeof(BrandResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Brand([FromBody] BrandRequest Brand)
        {
            try
            {
                var command = _mapper.Map<CreateBrandCommand>(Brand);
                var result = await _mediator.Send(command);
                return Created("api/brand", result);
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
        /// Get a brand by id.
        /// </summary>
        /// <param name="brandId">brand Id</param>
        /// <returns>The data from a brand by id </returns>
        /// <response code="200">Returns the brand</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response>   
        [HttpGet("{brandId}", Name = "GetBrandById")]
        [SwaggerOperation(Summary = "Get brand data", Description = "Request data from a brand by id")]
        [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Brand(Guid brandId)
        {
            try
            {
                var result = await _mediator.Send(new GetBrandByIdQuery(brandId));
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
        /// Get Brands.
        /// </summary>
        /// <returns>The data from a list of brands</returns>
        /// <response code="200">Returns the brand</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response>   
        [HttpGet(Name = "Brands")]
        [SwaggerOperation(Summary = "Get brand data", Description = "Request data from brands")]
        [ProducesResponseType(typeof(List<GetBrandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Brands()
        {
            try
            {
                var result = await _mediator.Send(new GetBrandsQuery());
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
        /// Update a brand.
        /// </summary>
        /// <returns>The Id from the updated brand </returns>
        /// <response code="200">Returns the brand Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpPut("{brandId}", Name = "UpdateBrand")]
        [SwaggerOperation(Summary = "Update a brand", Description = "Update a brand by id")]
        [ProducesResponseType(typeof(BrandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateBrand([FromRoute] Guid brandId, [FromBody] BrandRequest brand)
        {
            try
            {
                var result = await _mediator.Send(new UpdateBrandCommand(brandId, brand.Name));
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
        /// Delete a brand.
        /// </summary>
        /// <returns>The Id from the deleted brand </returns>
        /// <response code="200">Returns the brand Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpDelete("{brandId}", Name = "DeletedBrand")]
        [SwaggerOperation(Summary = "Delete a brand", Description = "Delete a brand by id")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteBrand([FromRoute] Guid brandId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteBrandCommand(brandId));
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
