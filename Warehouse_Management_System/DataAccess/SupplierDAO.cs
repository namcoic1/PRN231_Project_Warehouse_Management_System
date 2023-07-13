using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SupplierDAO
    {
        private static SupplierDAO _instance = null;
        private static MyDbContext _context = null;
        private SupplierDAO()
        {
        }
        public static SupplierDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SupplierDAO();
                }

                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Supplier> GetSuppliers() => _context.Suppliers.ToList();
        public Supplier GetSupplierById(string id) => _context.Suppliers.SingleOrDefault(c => c.Id.Equals(id));
        public Supplier GetSupplierByLastId() => _context.Suppliers.OrderBy(c => c.Id).LastOrDefault();

        public void SaveSupplier(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                _context.Entry<Supplier>(supplier).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteSupplier(Supplier supplier)
        {
            try
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
