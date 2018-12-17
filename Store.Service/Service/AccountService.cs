using Store.Service.Validation;
using Store.Model;
using Store.Model.DTO;
using Store.Repository;
using System.Linq;
using Jubin.Utils.Encryption;

namespace Store.Service
{
    public class AccountService : IAccountService
    {
        private IRepository _repo = RepositoryPlaceholder.CreateRepository();
        //private RepositoryPlaceholder _repo = new RepositoryPlaceholder();

        public OpResult CreateAccount(AccountCreateRequest dto)
        {

            Account account = new Account
            {
                UserName = dto.Username,
                Password = dto.Password,
                Email = dto.Email
            };

            AccountValidator validator = new AccountValidator();
            var validationResult = validator.Validate(account);

            if (!validationResult.IsValid)
            {
                return OpResult.FailureResult(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var existingAccount = _repo.GetFirst<Account>(a => a.UserName == dto.Username || a.Email == dto.Email);
            if (existingAccount != null)
            {
                if (existingAccount.UserName == dto.Username)
                    return OpResult.FailureResult("username already exists");
                if (existingAccount.Email == dto.Email)
                    return OpResult.FailureResult("eMail already exists");
            }

            var salt = EncryptionUtil.GenerateRandomSaltString();
            var hashedPass = EncryptionUtil.GeneratePBKDF2Hash(dto.Password, salt);

            account.PasswordHash = hashedPass;
            account.PasswordSalt = salt;

            _repo.Create<Account>(account);
            _repo.Save();

            return OpResult.SuccessResult();
        }

        public AccountResponse GetAccount(string userName)
        {
            var account = _repo.GetFirst<Account>(a => a.UserName == userName, null, "Profiles");
            if (account == null) return null;

            Profile profile = account.Profiles.Any() ? account.Profiles.Single() : null;

            return new AccountResponse
            {
                Email = account.Email,
                FirstName = profile?.FirstName,
                LastName = profile?.LastName
            };
        }

        public OpResult UpdateAccount(string userName, AccountUpdateRequest dto)
        {
            var account = _repo.GetFirst<Account>(a => a.UserName == userName, null, "Profiles");
            if (account == null) return OpResult.FailureResult("Account not found");

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

            if (account.Email != dto.Email)
            {
                var anotherAccount = _repo.GetFirst<Account>(a => a.ID != dto.Id && a.Email == dto.Email, null, "Profiles");
                if (anotherAccount != null)
                    return OpResult.FailureResult("eMail already exists");

                account.Email = dto.Email;
            }

            AccountValidator validator = new AccountValidator();
            var validationResult = validator.Validate(account);

            if (!validationResult.IsValid)
            {
                return OpResult.FailureResult(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            _repo.Update<Account>(account);
            _repo.Save();

            return OpResult.SuccessResult();
        }

        public AccountResponse GetAccount(int id)
        {
            var account = _repo.GetFirst<Account>(a => a.ID == id, null, "Profiles");
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
            var account = _repo.GetFirst<Account>(a => a.ID == dto.Id, null, "Profiles");
            if (account == null) return OpResult.FailureResult("Account not found");

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

            if (account.Email != dto.Email)
            {
                var anotherAccount = _repo.GetFirst<Account>(a => a.ID != dto.Id && a.Email == dto.Email, null, "Profiles");
                if (anotherAccount != null)
                    return OpResult.FailureResult("eMail already exists");

                account.Email = dto.Email;
            }

            AccountValidator validator = new AccountValidator();
            var validationResult = validator.Validate(account);

            if (!validationResult.IsValid)
            {
                return OpResult.FailureResult(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            _repo.Update<Account>(account);
            _repo.Save();

            return OpResult.SuccessResult();
        }



        

        public void DeleteAccount(int id)
        {
            _repo.Delete<Account>(id);
            _repo.Save();
        }
    }
}
