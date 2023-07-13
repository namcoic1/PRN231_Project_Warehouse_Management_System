using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CustomerDAO
    {
        private static CustomerDAO _instance = null;
        private static MyDbContext _context = null;
        private CustomerDAO()
        {
        }
        public static CustomerDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomerDAO();
                }

                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Customer> GetCustomers() => _context.Customers.ToList();
        public Customer GetCustomerById(string id) => _context.Customers.SingleOrDefault(c => c.Id.Equals(id));
        public Customer GetCustomerByLastId() => _context.Customers.OrderBy(c => c.Id).LastOrDefault();

        public void SaveCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
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
                _context.Entry<Customer>(customer).State = EntityState.Modified;
                _context.SaveChanges();
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
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
