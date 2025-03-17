using AutoMapper;
using FluentValidation;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface.Infrastructure;
using MarCorp.DemoBack.Application.Interface.Persistence;
using MarCorp.DemoBack.Application.Interface.UseCases;
using MarCorp.DemoBack.Application.Validator;
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

        public async Task<Response<bool>> Create(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
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
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                await _discountRepository.DeleteAsync(id.ToString());
                response.Data = await _discountRepository.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;                
            }
            return response;
        }

        public async Task<Response<DiscountDTO>> Get(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<DiscountDTO>();
            try
            {
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
            }
            catch (Exception e)
            {
                response.Message = e.Message;                
            }
            return response;
        }

        public async Task<Response<List<DiscountDTO>>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = new Response<List<DiscountDTO>>();
            try
            {
                var discounts = await _discountRepository.GetAllAsync(cancellationToken);
                response.Data = _mapper.Map<List<DiscountDTO>>(discounts);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<Response<bool>> Update(DiscountDTO discountDTO, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
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
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
