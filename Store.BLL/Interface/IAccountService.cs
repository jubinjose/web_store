using Store.Model;

namespace Store.BLL.Interface
{
    public interface IAccountService
    {
        OpResult<Account> CreateAccount(Account account);
    }
}
