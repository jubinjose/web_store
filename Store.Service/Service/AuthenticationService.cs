using Store.Service.Validation;
using Store.Model;
using Store.Model.DTO;
using Store.Repository;
using System.Linq;
using Jubin.Utils.Encryption;

namespace Store.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private IRepository _repo = RepositoryPlaceholder.CreateRepository();

        public OpResult LogOn(LoginRequest request)
        {
            LoginValidator validator = new LoginValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return OpResult.FailureResult(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var account = _repo.GetFirst<Account>(a => a.UserName == request.UserName);
            if (account==null)
                return OpResult.FailureResult("Account not found");

            var salt = account.PasswordSalt;
            var hashedPass = EncryptionUtil.GeneratePBKDF2Hash(request.Password, salt);

            if (account.PasswordHash != hashedPass)
                return OpResult.FailureResult("Invalid password");

            return OpResult<Account>.SuccessResult(account);
        }
    }
}
