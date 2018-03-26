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

        public OpResult CreateAccount(AccountCreateRequest dto)
        {

            Account account = new Account
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Email = dto.Email
            };

            AccountValidator validator = new AccountValidator();
            var validationResult = validator.Validate(account);

            if (!validationResult.IsValid)
            {
                return OpResult.FailureResult(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var salt = Jubin.Utility.EncryptionUtility.GenerateRandomSaltString();
            var hashedPass = Jubin.Utility.EncryptionUtility.GeneratePBKDF2Hash(dto.Password, salt);

            account.PasswordHash = hashedPass;
            account.PasswordSalt = salt;

            _repo.SaveAccount(account);

            return OpResult.SuccessResult();
        }

        public AccountResponse GetAccount(int id)
        {
            var account = _repo.GetAccount(id);
            if (account == null) return null;

            Profile profile = account.Profiles.Any() ? account.Profiles.Single() : null;

            return new AccountResponse
            {
                Email = account.Email,
                FirstName = profile?.FirstName,
                LastName = profile?.LastName
            };
        }

        public OpResult UpdateAccount(AccountUpdateRequest dto)
        {
            var account = _repo.GetAccount(dto.Id);
            if (account == null) return OpResult.FailureResult("Account not found");

            account.Email = dto.Email;


            var profile = account.GetProfile();

            if (profile == null)
            {
                if (!(string.IsNullOrEmpty(dto.FirstName) && string.IsNullOrWhiteSpace(dto.LastName)))
                {
                    profile = new Profile();
                    account.Profiles.Add(profile);
                }
            }
            if (profile != null)
            {
                profile.FirstName = dto.FirstName;
                profile.LastName = dto.LastName;
            }

            account.Password = "dummy"; // To pass validation

            AccountValidator validator = new AccountValidator();
            var validationResult = validator.Validate(account);

            if (!validationResult.IsValid)
            {
                return OpResult.FailureResult(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            _repo.SaveAccount(account);

            return OpResult.SuccessResult();
        }

        public bool DeleteAccount(int id)
        {
            return _repo.DeleteAccount(id);
        }
    }
}
