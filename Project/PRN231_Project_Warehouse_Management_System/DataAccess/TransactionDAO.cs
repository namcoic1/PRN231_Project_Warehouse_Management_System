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

        public List<Transaction> GetTransactions() => MyDB_Context.GetInstance().Transactions.ToList();
        public Transaction GetTransactionById(int id) => MyDB_Context.GetInstance().Transactions.SingleOrDefault(c => c.TransactionID == id);

        public void SaveTransaction(Transaction transaction)
        {
            try
            {
                MyDB_Context.GetInstance().Transactions.Add(transaction);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<Transaction>(transaction).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Transactions.Remove(transaction);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
