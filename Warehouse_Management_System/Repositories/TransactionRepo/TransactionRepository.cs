﻿using BusinessObjects;
using DataAccess;

namespace Repositories.TransactionRepo
{
    public class TransactionRepository : ITransactionRepository
    {
        public void SaveTransaction(Transaction transaction) => TransactionDAO.GetInstance.SaveTransaction(transaction);

        public void DeleteTransaction(Transaction transaction) => TransactionDAO.GetInstance.DeleteTransaction(transaction);

        public List<Transaction> GetTransactionsByUserId(int? id) => TransactionDAO.GetInstance.GetTransactionsByUserId(id);

        public Transaction GetTransactionById(int id) => TransactionDAO.GetInstance.GetTransactionById(id);

        public Transaction GetTransactionByLastId() => TransactionDAO.GetInstance.GetTransactionByLastId();

        public List<Transaction> GetTransactions() => TransactionDAO.GetInstance.GetTransactions();

        public void UpdateTransaction(Transaction transaction) => TransactionDAO.GetInstance.UpdateTransaction(transaction);
    }
}
