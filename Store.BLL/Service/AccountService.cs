using Store.BLL.Interface;
using Store.BLL.Validation;
using Store.Model;
using Store.Model.DTO;
using Store.Repository;
using System.Linq;

namespace Store.BLL.Service
{
    public class AccountService : IAccountService
    {
        private RepositoryPlaceholder _repo = new RepositoryPlaceholder();

        public OpResult CreateAccount(AccountCreateDto dto)
        {
            AccountCreateValidator validator = new AccountCreateValidator();
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                return OpResult.FailureResult(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var salt = Jubin.Utility.EncryptionUtility.GenerateRandomSaltString();
            var hashedPass = Jubin.Utility.EncryptionUtility.GeneratePBKDF2Hash(dto.Password, salt);

            Account account = new Account
            {
                UserName = dto.UserName,
                PasswordHash = hashedPass,
                PasswordSalt = salt,
                Email = dto.Email
            };

            _repo.CreateAccount(account);

            return OpResult.SuccessResult();
        }

        public AccountDTO GetAccount(int id)
        {
            var account = _repo.GetAccount(id);
            if (account == null) return null;

            Profile profile = account.Profiles.Any() ? account.Profiles.Single() : null;

            return new AccountDTO
            {
                Email = account.Email,
                FirstName = profile?.FirstName,
                LastName = profile?.LastName
            };
        }

        public bool DeleteAccount(int id)
        {
            return _repo.DeleteAccount(id);
        }
    }
}
