using Store.Model;
using Store.Model.DTO;

namespace Store.BLL.Interface
{
    public interface IAccountService
    {
        OpResult CreateAccount(AccountCreateRequest dto);

        AccountResponse GetAccount(int id);

        OpResult UpdateAccount(AccountUpdateRequest dto);

        bool DeleteAccount(int id);
    }
}
