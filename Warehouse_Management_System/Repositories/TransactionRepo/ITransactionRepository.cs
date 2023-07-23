using BusinessObjects;

namespace Repositories.TransactionRepo
{
    public interface ITransactionRepository
    {
        List<Transaction> GetTransactions();
        List<Transaction> GetTransactionsByUserId(int? id);
        Transaction GetTransactionById(int id);
        Transaction GetTransactionByLastId();
        void SaveTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(Transaction transaction);
    }
}
