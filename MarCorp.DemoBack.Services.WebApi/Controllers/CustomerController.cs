using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarCorp.DemoBack.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using MarCorp.DemoBack.Application.Interface.UseCases;

namespace MarCorp.DemoBack.Services.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing customer operations.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="customerApplication">The customer application service.</param>
        public CustomerController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }

        #region "Métodos Sincronos"

        /// <summary>
        /// Inserts a new customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>The result of the insert operation.</returns>
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = _customerApplication.Insert(customerDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("Update")]
        public IActionResult Update([FromBody] CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = _customerApplication.Update(customerDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete("Delete/{customerId}")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = _customerApplication.Delete(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Gets a customer by ID.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>The customer data.</returns>
        [HttpGet("Get/{customerId}")]
        public IActionResult Get(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = _customerApplication.Get(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>The list of all customers.</returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = _customerApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
        #endregion

        #region "Métodos Asincronos"

        /// <summary>
        /// Asynchronously inserts a new customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>The result of the insert operation.</returns>
        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = await _customerApplication.InsertAsync(customerDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Asynchronously updates an existing customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomerDTO customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = await _customerApplication.UpdateAsync(customerDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Asynchronously deletes a customer by ID.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete("DeleteAsync/{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = await _customerApplication.DeleteAsync(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Asynchronously gets a customer by ID.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>The customer data.</returns>
        [HttpGet("GetAsync/{customerId}")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = await _customerApplication.GetAsync(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        /// <summary>
        /// Asynchronously gets all customers.
        /// </summary>
        /// <returns>The list of all customers.</returns>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customerApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }

        #endregion

    }
}
