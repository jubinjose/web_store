using Store.Model;
using System.Data.Entity;
using System.Linq;

namespace Store.Repository
{
    public class RepositoryPlaceholder
    {
        public static EFRepository<DataStoreConnection> CreateRepository()
        {
            //Because I am too lazy to do DI and inject EFRepository in my Service classes
            return new EFRepository<DataStoreConnection>(new DataStoreConnection());
        }

        private DataStoreConnection _context = new DataStoreConnection();

        public int SaveAccount(Account account)
        {
            if (account.ID == 0)
            {
                _context.Accounts.Add(account); //Create
            }
            _context.SaveChanges();
            return account.ID;
        }

        public Account GetAccount(int id)
        {
            var account = _context.Accounts.Where(a => a.ID == id)
                                 .Include(a => a.Profiles)
                                 .SingleOrDefault();

            return account;
        }

        public void DeleteAccountFaster(int id)
        { 
            // No need to load data unnecessarily and use that record to delete. EF only cares about ID
            var account = new Account { ID = id };
            _context.Entry(account).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public bool DeleteAccount(int id)
        {
            var itemToRemove = _context.Accounts.SingleOrDefault(a => a.ID == id);

            if (itemToRemove != null)
            {
                _context.Accounts.Remove(itemToRemove);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
