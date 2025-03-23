using AutoMapper;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Domain.Models.Entities;
using MarCorp.DemoBack.Support.Common;
using MarCorp.DemoBack.Application.Interface.Persistence;
using MarCorp.DemoBack.Application.Interface.UseCases;

namespace MarCorp.DemoBack.Application.UseCases.Customers
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CustomersApplication> _logger;
        public CustomersApplication(ICustomersRepository customersRepository, IMapper mapper, IAppLogger<CustomersApplication> logger)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
            _logger = logger;
        }

        #region Métodos Síncronos

        public Response<bool> Insert(CustomerDTO customersDto)
        {
            var response = new Response<bool>();
            
            var customer = _mapper.Map<Customer>(customersDto);
            response.Data = _customersRepository.Insert(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";
            }
            
            return response;
        }

        public Response<bool> Update(CustomerDTO customersDto)
        {
            var response = new Response<bool>();
            
            var customer = _mapper.Map<Customer>(customersDto);
            response.Data = _customersRepository.Update(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualización Exitosa!!!";
            }
            
            return response;
        }

        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();
            
            response.Data = _customersRepository.Delete(customerId);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación Exitosa!!!";
            }
            
            return response;
        }

        public Response<CustomerDTO> Get(string customerId)
        {
            var response = new Response<CustomerDTO>();
            
            var customer = _customersRepository.Get(customerId);
            response.Data = _mapper.Map<CustomerDTO>(customer);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }
            
            return response;
        }

        public Response<IEnumerable<CustomerDTO>> GetAll()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();
            
            var customers = _customersRepository.GetAll();
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
                _logger.LogInformation(response.Message);
            }
            
            return response;
        }

        #endregion

        #region Métodos Asíncronos
        public async Task<Response<bool>> InsertAsync(CustomerDTO customersDto)
        {
            var response = new Response<bool>();
            
            var customer = _mapper.Map<Customer>(customersDto);
            response.Data = await _customersRepository.InsertAsync(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";
            }
            
            return response;
        }

        public async Task<Response<bool>> UpdateAsync(CustomerDTO customersDto)
        {
            var response = new Response<bool>();
            
            var customer = _mapper.Map<Customer>(customersDto);
            response.Data = await _customersRepository.UpdateAsync(customer);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualización Exitosa!!!";
            }
            
            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();
            
            response.Data = await _customersRepository.DeleteAsync(customerId);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación Exitosa!!!";
            }
            
            return response;
        }

        public async Task<Response<CustomerDTO>> GetAsync(string customerId)
        {
            var response = new Response<CustomerDTO>();
            
            var customer = await _customersRepository.GetAsync(customerId);
            response.Data = _mapper.Map<CustomerDTO>(customer);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }
            
            return response;
        }

        public async Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDTO>>();
            
            var customers = await _customersRepository.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa";
            }
           
            return response;
        }
        #endregion
    }
}
