using Store.Model;
using Store.Model.DTO;

namespace Store.BLL.Interface
{
    public interface IAccountService
    {
        OpResult CreateAccount(AccountCreateDto dto);

        AccountDTO GetAccount(int id);

        bool DeleteAccount(int id);
    }
}
