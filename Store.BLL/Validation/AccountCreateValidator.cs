using FluentValidation;
using Store.Model.DTO;

namespace Store.BLL.Validation
{
    public class AccountCreateValidator: AbstractValidator<AccountCreateDto>
    {
        public AccountCreateValidator()
        {
            RuleFor(r => r.UserName).NotEmpty().WithMessage("UserName is required")
                            .Length(3, 128).WithMessage("UserName must be between 3 to 128 in length");

            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is required")
                            .Length(4, 128).WithMessage("Password must be between 8 to 128 in length");

            RuleFor(r => r.Email).NotEmpty().WithMessage("Email address is required");

            RuleFor(r => r.Email).EmailAddress().WithMessage("Invalid Email address")
                .When(r => !string.IsNullOrEmpty(r.Email));
        }
    }
}
