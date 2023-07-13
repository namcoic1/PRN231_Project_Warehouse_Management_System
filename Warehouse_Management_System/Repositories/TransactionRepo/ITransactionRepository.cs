using BusinessObjects;

namespace Repositories.TransactionRepo
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions();
        Transaction GetTransactionById(int id);
        Transaction GetTransactionByLastId();
        void SaveTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(Transaction transaction);
    }
}
