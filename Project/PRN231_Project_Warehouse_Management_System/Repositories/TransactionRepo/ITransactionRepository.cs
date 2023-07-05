using BusinessObjects;

namespace Repositories.TransactionRepo
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions();
        Transaction GetTransactionById(int id);
        void SaveTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(Transaction transaction);
    }
}
