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

        public List<Customer> GetCustomers() => MyDbContext.GetInstance().Customers.ToList();
        public Customer GetCustomerById(string id) => MyDbContext.GetInstance().Customers.SingleOrDefault(c => c.ID == id);

        public void SaveCustomer(Customer customer)
        {
            try
            {
                MyDbContext.GetInstance().Customers.Add(customer);
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Entry<Customer>(customer).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Customers.Remove(customer);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
