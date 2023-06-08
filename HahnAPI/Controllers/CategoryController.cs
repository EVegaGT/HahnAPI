using AutoMapper;
using Domain.Common.Exceptions;
using Domain.Models.Requests.Category;
using Domain.Models.Responses.Category;
using Domain.Services.Category.Components;
using Domain.Services.Category.Queries;
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
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a category by id.
        /// </summary>
        /// <param name="categoryId">category Id</param>
        /// <returns>The data from a category by id </returns>
        /// <response code="200">Returns the category</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response>   
        [HttpGet("{categoryId}", Name = "GetCategoryById")]
        [SwaggerOperation(Summary = "Get category data", Description = "Request data from a category by id")]
        [ProducesResponseType(typeof(GetCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Category(Guid categoryId)
        {
            try
            {
                var result = await _mediator.Send(new GetCategoryByIdQuery(categoryId));
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
        /// Get Categories.
        /// </summary>
        /// <returns>The data from a list of categories</returns>
        /// <response code="200">Returns the category</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response>   
        [HttpGet(Name = "Categories")]
        [SwaggerOperation(Summary = "Get category data", Description = "Request data from categories")]
        [ProducesResponseType(typeof(List<GetCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Categories()
        {
            try
            {
                var result = await _mediator.Send(new GetCategoriesQuery());
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
        /// Create a new category.
        /// </summary>
        /// <returns>The Id from the newly category </returns>
        /// <response code="201">Returns the category Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpPost(Name = "CreateCategory")]
        [SwaggerOperation(Summary = "Create a category", Description = "Create a new category")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> Category([FromBody] CategoryRequest Category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                var command = _mapper.Map<CreateCategoryCommand>(Category);
                var result = await _mediator.Send(command);
                return Created("api/category", result);
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
        /// Update a category.
        /// </summary>
        /// <returns>The Id from the updated category </returns>
        /// <response code="200">Returns the category Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpPut("{categoryId}", Name = "UpdateCategory")]
        [SwaggerOperation(Summary = "Update a category", Description = "Update a category by id")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] CategoryRequest category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                var result = await _mediator.Send(new UpdateCategoryCommand(categoryId, category.Name));
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
        /// Delete a category.
        /// </summary>
        /// <returns>The Id from the deleted category </returns>
        /// <response code="200">Returns the category Id</response>
        /// <response code="400">If an error occurs</response>   
        /// <response code="401">If it is unauthorized</response> 
        /// <response code="403">If the user is not authorized to access this</response> 
        [HttpDelete("{categoryId}", Name = "DeletedCategory")]
        [SwaggerOperation(Summary = "Delete a category", Description = "Delete a category by id")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCategoryCommand(categoryId));
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
