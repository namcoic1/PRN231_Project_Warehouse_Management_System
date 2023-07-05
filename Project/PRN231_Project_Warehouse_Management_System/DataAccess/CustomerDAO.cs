using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CustomerDAO
    {
        private static CustomerDAO _instance = null;
        private CustomerDAO() { }
        public static CustomerDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CustomerDAO();
            }
            return _instance;
        }

        public List<Customer> GetCustomers() => MyDB_Context.GetInstance().Customers.ToList();
        public Customer GetCustomerById(string id) => MyDB_Context.GetInstance().Customers.SingleOrDefault(c => c.CustomerID == id);

        public void SaveCustomer(Customer customer)
        {
            try
            {
                MyDB_Context.GetInstance().Customers.Add(customer);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            try
            {
                MyDB_Context.GetInstance().Entry<Customer>(customer).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteCustomer(Customer customer)
        {
            try
            {
                MyDB_Context.GetInstance().Customers.Remove(customer);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
