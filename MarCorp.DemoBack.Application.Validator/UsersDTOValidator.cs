using FluentValidation;
using MarCorp.DemoBack.Application.DTO;

namespace MarCorp.DemoBack.Application.Validator
{
    public class UsersDTOValidator : AbstractValidator<UsersDTO>
    {
        public UsersDTOValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");
        }
    }
}
