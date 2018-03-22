using Store.BLL.Interface;
using Store.Model;
using Store.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Service
{
    public class AccountService : IAccountService
    {
        public OpResult<Account> CreateAccount(Account account)
        {
            return new RepositoryPlaceholder().CreateAccount(account);
        }
    }
}
