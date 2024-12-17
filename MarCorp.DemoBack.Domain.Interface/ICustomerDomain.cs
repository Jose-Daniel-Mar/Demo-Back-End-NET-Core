using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarCorp.DemoBack.Domain.Models.Entities;
using System.Threading.Tasks;

namespace MarCorp.DemoBack.Domain.Interface
{
    public interface ICustomerDomain
    {
        #region Métodos Síncronos

        bool Insert(Customer customer);
        bool Update(Customer customer);
        bool Delete(string customerId);
        Customer Get(string customerId);
        IEnumerable<Customer> GetAll();

        #endregion

        #region Métodos Asíncronos

        Task<bool> InsertAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(string customerId);
        Task<Customer> GetAsync(string customerId);
        Task<IEnumerable<Customer>> GetAllAsync();

        #endregion
    }
}
