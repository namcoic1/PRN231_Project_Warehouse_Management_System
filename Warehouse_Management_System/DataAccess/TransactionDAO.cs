using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TransactionDAO
    {
        private static TransactionDAO _instance = null;
        private static MyDbContext _context = null;
        private TransactionDAO()
        {
        }
        public static TransactionDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TransactionDAO();
                }
                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Transaction> GetTransactions() => _context.Transactions.Include(c => c.Customer).Include(c => c.Carrier)
            .Include(c => c.Supplier).Include(c => c.User).Include(c => c.Location).Include(c => c.Product).ToList();
        public Transaction GetTransactionById(int id) => _context.Transactions.Include(c => c.Customer).Include(c => c.Carrier)
            .Include(c => c.Supplier).Include(c => c.User).Include(c => c.Location).Include(c => c.Product).SingleOrDefault(c => c.Id == id);
        public Transaction GetTransactionByLastId() => _context.Transactions.Include(c => c.Customer).Include(c => c.Carrier)
            .Include(c => c.Supplier).Include(c => c.User).Include(c => c.Location).Include(c => c.Product).OrderBy(c => c.Id).LastOrDefault();

        public void SaveTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateTransaction(Transaction transaction)
        {
            try
            {
                _context.Entry<Transaction>(transaction).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
