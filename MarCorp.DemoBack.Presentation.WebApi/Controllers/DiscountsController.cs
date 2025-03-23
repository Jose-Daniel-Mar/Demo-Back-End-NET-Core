using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MarCorp.DemoBack.Presentation.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing discounts.
    /// </summary>
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountsApplication _discountsApplication;

        /// <summary>
        /// Asynchronously Initializes a new instance of the <see cref="DiscountsController"/> class.
        /// </summary>
        /// <param name="discountsApplication">The discounts application service.</param>
        public DiscountsController(IDiscountsApplication discountsApplication)
        {
            _discountsApplication = discountsApplication;
        }

        /// <summary>
        /// Asynchronously Creates a new discount.
        /// </summary>
        /// <param name="discountDTO">The discount data transfer object.</param>
        /// <returns>The result of the creation operation.</returns>
        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromBody] DiscountDTO discountDTO)
        {
            if (discountDTO == null)
                return BadRequest();
            var response = await _discountsApplication.CreateAsync(discountDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Asynchronously Updates an existing discount.
        /// </summary>
        /// <param name="id">The discount identifier.</param>
        /// <param name="discountDTO">The discount data transfer object.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("UpdateAsync/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] DiscountDTO discountDTO)
        {
            var customerDTOExists = await _discountsApplication.GetAsync(id);
            if (customerDTOExists.Data == null)
                return NotFound(customerDTOExists);

            if (discountDTO == null)
                return BadRequest();
            var response = await _discountsApplication.UpdateAsync(discountDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Asynchronously Deletes a discount.
        /// </summary>
        /// <param name="id">The discount identifier.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _discountsApplication.DeleteAsync(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Asynchronously Gets a discount by identifier.
        /// </summary>
        /// <param name="id">The discount identifier.</param>
        /// <returns>The discount data transfer object.</returns>
        [HttpGet("GetAsync/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var response = await _discountsApplication.GetAsync(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Asynchronously Gets all discounts.
        /// </summary>
        /// <returns>A list of discount data transfer objects.</returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _discountsApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
