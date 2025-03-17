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
        /// Initializes a new instance of the <see cref="DiscountsController"/> class.
        /// </summary>
        /// <param name="discountsApplication">The discounts application service.</param>
        public DiscountsController(IDiscountsApplication discountsApplication)
        {
            _discountsApplication = discountsApplication;
        }

        /// <summary>
        /// Creates a new discount.
        /// </summary>
        /// <param name="discountDTO">The discount data transfer object.</param>
        /// <returns>The result of the creation operation.</returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DiscountDTO discountDTO)
        {
            if (discountDTO == null)
                return BadRequest();
            var response = await _discountsApplication.Create(discountDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Updates an existing discount.
        /// </summary>
        /// <param name="id">The discount identifier.</param>
        /// <param name="discountDTO">The discount data transfer object.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DiscountDTO discountDTO)
        {
            var customerDTOExists = await _discountsApplication.Get(id);
            if (customerDTOExists.Data == null)
                return NotFound(customerDTOExists);

            if (discountDTO == null)
                return BadRequest();
            var response = await _discountsApplication.Update(discountDTO);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Deletes a discount.
        /// </summary>
        /// <param name="id">The discount identifier.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _discountsApplication.Delete(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Gets a discount by identifier.
        /// </summary>
        /// <param name="id">The discount identifier.</param>
        /// <returns>The discount data transfer object.</returns>
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _discountsApplication.Get(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Gets all discounts.
        /// </summary>
        /// <returns>A list of discount data transfer objects.</returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _discountsApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
