using AutoMapper;
using FluentValidation;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface.Infrastructure;
using MarCorp.DemoBack.Application.Interface.Persistence;
using MarCorp.DemoBack.Application.Interface.UseCases;
using MarCorp.DemoBack.Domain.Models.Entities;
using MarCorp.DemoBack.Domain.Models.Events;
using MarCorp.DemoBack.Support.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Discounts
{
    public class DiscountsApplication : IDiscountsApplication
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;
        private readonly IValidator<DiscountDTO> _discountDTOValidator;

        public DiscountsApplication(IDiscountRepository discountRepository, IMapper mapper, IEventBus eventBus, IValidator<DiscountDTO> discountDTOValidator)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _eventBus = eventBus;
            _discountDTOValidator = discountDTOValidator;
        }

        public async Task<Response<bool>> CreateAsync(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            
            // Validacion de los datos de entrada
            var validation = await _discountDTOValidator.ValidateAsync(discountDTO, cancellationToken);
            if (!validation.IsValid)
            {
                response.Message = "Errores de Validación";
                response.Errors = validation.Errors;
                return response;
            }
                
            // Mapeo de los datos de entrada
            var discount = _mapper.Map<Discount>(discountDTO);
                
            await _discountRepository.InsertAsync(discount);

            response.Data = await _discountRepository.Save(cancellationToken)>0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";

                /* Publicamos el evento */
                var discountCreatedEvent = _mapper.Map<DiscountCreatedEvent>(discount);
                _eventBus.Publish(discountCreatedEvent);
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            
            var validation = await _discountDTOValidator.ValidateAsync(discountDTO, cancellationToken);
            if (!validation.IsValid)
            {
                response.Message = "Errores de Validación";
                response.Errors = validation.Errors;
                return response;
            }
            var discount = _mapper.Map<Discount>(discountDTO);
            await _discountRepository.UpdateAsync(discount);

            response.Data = await _discountRepository.Save(cancellationToken) > 0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualización Exitosa!!!";
            }
            
            return response;
        }

        public async Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            
            await _discountRepository.DeleteAsync(id.ToString());
            response.Data = await _discountRepository.Save(cancellationToken) > 0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación Exitosa!!!";
            }
            
            return response;
        }

        public async Task<Response<DiscountDTO>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<DiscountDTO>();
            
            var discount = await _discountRepository.GetAsync(id, cancellationToken);
            if (discount is null)
            {
                response.IsSuccess = true;
                response.Message = "Descuento no existe...";
                return response;
            }

            response.Data = _mapper.Map<DiscountDTO>(discount);
            response.IsSuccess = true;
            response.Message = "Consulta Exitosa!!!";
            
            return response;
        }

        public async Task<Response<List<DiscountDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<List<DiscountDTO>>();
            
            var discounts = await _discountRepository.GetAllAsync(cancellationToken);
            response.Data = _mapper.Map<List<DiscountDTO>>(discounts);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }
           
            return response;
        }
    }
}
