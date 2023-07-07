using BusinessObjects;
using DataAccess;

namespace Repositories.TransactionRepo
{
    public class TransactionRepository : ITransactionRepository
    {
        public void SaveTransaction(Transaction transaction) => TransactionDAO.GetInstance().SaveTransaction(transaction);

        public void DeleteTransaction(Transaction transaction) => TransactionDAO.GetInstance().DeleteTransaction(transaction);

        public Transaction GetTransactionById(int id) => TransactionDAO.GetInstance().GetTransactionById(id);

        public List<Transaction> GetTransactions() => TransactionDAO.GetInstance().GetTransactions();

        public void UpdateTransaction(Transaction transaction) => TransactionDAO.GetInstance().UpdateTransaction(transaction);
    }
}
