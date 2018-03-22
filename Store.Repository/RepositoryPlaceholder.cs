using Store.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class RepositoryPlaceholder
    {
        public OpResult<Account> CreateAccount(Account account)
        {
            using (var context = new DataStoreConnection())
            {
                context.Accounts.Add(account);
                context.SaveChanges();
                return OpResult<Account>.SuccessResult(account);
            }
        }
    }
}
