using FluentValidation;
using Store.Model.DTO;

namespace Store.Service.Validation
{
    public class LoginValidator : BaseValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(r => r.UserName).NotEmpty().WithMessage("UserName is required");

            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is required");
                            
        }
    }
}
