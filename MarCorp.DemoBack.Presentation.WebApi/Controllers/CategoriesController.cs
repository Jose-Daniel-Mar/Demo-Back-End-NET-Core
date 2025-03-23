using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface.UseCases;
using MarCorp.DemoBack.Support.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;

namespace MarCorp.DemoBack.Presentation.WebApi.Controllers
{
    /// <summary>
    /// Controller to manage categories of products.
    /// </summary>
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Get Categories of Products")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesApplication _categoriesApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="categoriesApplication">The categories application service.</param>
        public CategoriesController(ICategoriesApplication categoriesApplication)
        {
            _categoriesApplication = categoriesApplication;
        }

        /// <summary>
        /// Gets all categories asynchronously.
        /// </summary>
        /// <returns>A list of categories.</returns>
        [HttpGet("GetAllAsync")]
        [SwaggerOperation(
            Summary = "Get all categories",
            Description = "This enpoint will return all categories",
            OperationId = "GetAllAsync",
            Tags = new[] { "Categories", "GetAllAsync" }
        )]
        [SwaggerResponse(200, "The request was successful List of Categories", typeof(Response<IEnumerable<CategoryDTO>>))]
        [SwaggerResponse(400, "The request was not successful", typeof(string))]
        [SwaggerResponse(404, "Not Found Categories", typeof(string))]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _categoriesApplication.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
    }
}
