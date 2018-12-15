using Store.BLL.Interface;
using Store.BLL.Validation;
using Store.Model;
using Store.Model.DTO;
using Store.Repository;
using System;
using System.Linq;

namespace Store.BLL.Service
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
            var hashedPass = Jubin.Utility.EncryptionUtility.GeneratePBKDF2Hash(request.Password, salt);

            if (account.PasswordHash != hashedPass)
                return OpResult.FailureResult("Invalid password");

            return OpResult<Account>.SuccessResult(account);
        }
    }
}
