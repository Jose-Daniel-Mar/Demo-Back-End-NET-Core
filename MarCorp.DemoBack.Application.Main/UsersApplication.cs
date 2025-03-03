using AutoMapper;
using FluentValidation;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface;
using MarCorp.DemoBack.Domain.Interface;
using MarCorp.DemoBack.Support.Common;

namespace MarCorp.DemoBack.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUsersDomain _usersDomain;
        private readonly IMapper _mapper;
        private readonly IValidator<UsersDTO> _usersDtoValidator;

        public UsersApplication(IUsersDomain usersDomain, IMapper iMapper, IValidator<UsersDTO> usersDtoValidator)
        {
            _usersDomain = usersDomain;
            _mapper = iMapper;
            _usersDtoValidator = usersDtoValidator;
        }
        public async Task<Response<UsersDTO>> AuthenticateAsync(string username, string password)
        {
            var response = new Response<UsersDTO>();
            var validation = _usersDtoValidator.Validate(new UsersDTO { UserName = username, Password = password });

            if (!validation.IsValid)
            {
                response.Message = "Errores de validación";
                response.Errors = validation.Errors;
                return response;
            }
            try
            {
                var user = await _usersDomain.AuthenticateAsync(username, password);
                response.Data = _mapper.Map<UsersDTO>(user);
                response.IsSuccess = true;
                response.Message = "Autenticación Exitosa!!!";
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
