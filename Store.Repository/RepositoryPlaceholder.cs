using Store.Model;
using System.Data.Entity;
using System.Linq;

namespace Store.Repository
{
    public class RepositoryPlaceholder
    {
        public int SaveAccount(Account account)
        {
            using (var context = new DataStoreConnection())
            {
                if (account.ID == 0) context.Accounts.Add(account); //To handle Create and Update
                context.SaveChanges();
                return account.ID;
            }
        }

        public Account GetAccount(int id)
        {
            using (var context = new DataStoreConnection())
            {
                var account = context.Accounts.Where(a => a.ID == id)
                                .Include(a => a.Profiles)
                                .SingleOrDefault();

                return account;
            }
        }

        public void DeleteAccountFaster(int id)
        { 
            using (var context = new DataStoreConnection())
            {
                // No need to load data unnecessarily and use that record to delete. EF only cares about ID
                var account = new Account { ID = id };
                context.Entry(account).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public bool DeleteAccount(int id)
        {
            using (var context = new DataStoreConnection())
            {
                var itemToRemove = context.Accounts.SingleOrDefault(a => a.ID == id); 

                if (itemToRemove != null)
                {
                    context.Accounts.Remove(itemToRemove);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
