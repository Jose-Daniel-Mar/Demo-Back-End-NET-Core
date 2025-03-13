using MarCorp.DemoBack.Data.Interface;
using MarCorp.DemoBack.Domain.Interface;
using MarCorp.DemoBack.Domain.Models.Entities;

namespace MarCorp.DemoBack.Domain.Core
{
    public class CustomerDomain(ICustomerRepository customersRepository) : ICustomerDomain
    {
        // Aqui van las reglas de negocio aplicadas a los métodos de la interfaz ICustomerRepository de la capa de datos
        private readonly ICustomerRepository _customerRepository = customersRepository;

        #region Métodos Síncronos

        public bool Insert(Customer customers)
        {
            // Logica de negocio
            return _customerRepository.Insert(customers);
        }

        public bool Update(Customer customers)
        {
            // Logica de negocio
            return _customerRepository.Update(customers);
        }

        public bool Delete(string customerId)
        {
            // Logica de negocio
            return _customerRepository.Delete(customerId);
        }

        public Customer Get(string customerId)
        {
            // Logica de negocio
            return _customerRepository.Get(customerId);
        }

        public IEnumerable<Customer> GetAll()
        {
            // Logica de negocio
            return _customerRepository.GetAll();
        }
        
        #endregion

        #region Métodos Asíncronos

        public async Task<bool> InsertAsync(Customer customers)
        {
            // Logica de negocio
            return await _customerRepository.InsertAsync(customers);
        }

        public async Task<bool> UpdateAsync(Customer customers)
        {
            // Logica de negocio
            return await _customerRepository.UpdateAsync(customers);
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            // Logica de negocio
            return await _customerRepository.DeleteAsync(customerId);
        }

        public async Task<Customer> GetAsync(string customerId)
        {
            // Logica de negocio
            return await _customerRepository.GetAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            // Logica de negocio AllAsync
            return await _customerRepository.GetAllAsync();
        }

        #endregion
    }
}
