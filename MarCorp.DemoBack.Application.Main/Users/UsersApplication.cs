using AutoMapper;
using FluentValidation;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface.UseCases;
using MarCorp.DemoBack.Application.Interface.Persistence;
using MarCorp.DemoBack.Support.Common;

namespace MarCorp.DemoBack.Application.UseCases.Users
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UserDTO> _usersDtoValidator;

        public UsersApplication(IUsersRepository usersRepository, IMapper iMapper, IValidator<UserDTO> usersDTOValidator)
        {
            _usersRepository = usersRepository;
            _mapper = iMapper;
            _usersDtoValidator = usersDTOValidator;
        }
        public async Task<Response<UserDTO>> AuthenticateAsync(string username, string password)
        {
            var response = new Response<UserDTO>();
            var validation = _usersDtoValidator.Validate(new UserDTO { UserName = username, Password = password });

            if (!validation.IsValid)
            {
                response.Message = "Errores de validación";
                response.Errors = validation.Errors;
                return response;
            }
            try
            {
                var user = await _usersRepository.AuthenticateAsync(username, password);
                response.Data = _mapper.Map<UserDTO>(user);
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
