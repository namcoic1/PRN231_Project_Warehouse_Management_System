using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TransactionDAO
    {
        private static TransactionDAO _instance = null;
        private TransactionDAO() { }
        public static TransactionDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TransactionDAO();
            }
            return _instance;
        }

        public List<Transaction> GetTransactions() => MyDbContext.GetInstance().Transactions.ToList();
        public Transaction GetTransactionById(int id) => MyDbContext.GetInstance().Transactions.SingleOrDefault(c => c.ID == id);

        public void SaveTransaction(Transaction transaction)
        {
            try
            {
                MyDbContext.GetInstance().Transactions.Add(transaction);
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Entry<Transaction>(transaction).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Transactions.Remove(transaction);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
