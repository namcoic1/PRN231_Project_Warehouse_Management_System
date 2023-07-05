using BusinessObjects;
using DataAccess;

namespace Repositories.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        public void SaveCustomer(Customer customer) => CustomerDAO.GetInstance().SaveCustomer(customer);

        public void DeleteCustomer(Customer customer) => CustomerDAO.GetInstance().DeleteCustomer(customer);

        public Customer GetCustomerById(string id) => CustomerDAO.GetInstance().GetCustomerById(id);

        public List<Customer> GetCustomers() => CustomerDAO.GetInstance().GetCustomers();

        public void UpdateCustomer(Customer customer) => CustomerDAO.GetInstance().UpdateCustomer(customer);
    }
}
