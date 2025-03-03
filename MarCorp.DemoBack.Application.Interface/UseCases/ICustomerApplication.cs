using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Support.Common;

namespace MarCorp.DemoBack.Application.Interface.UseCases
{
    public interface ICustomerApplication
    {
        #region Métodos Síncronos

        Response<bool> Insert(CustomerDTO customersDto);
        Response<bool> Update(CustomerDTO customersDto);
        Response<bool> Delete(string customerId);

        Response<CustomerDTO> Get(string customerId);
        Response<IEnumerable<CustomerDTO>> GetAll();

        #endregion

        #region Métodos Asíncronos
        Task<Response<bool>> InsertAsync(CustomerDTO customersDto);
        Task<Response<bool>> UpdateAsync(CustomerDTO customersDto);
        Task<Response<bool>> DeleteAsync(string customerId);

        Task<Response<CustomerDTO>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();

        #endregion
    }
}
