using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SupplierDAO
    {
        private static SupplierDAO _instance = null;
        private SupplierDAO() { }
        public static SupplierDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SupplierDAO();
            }
            return _instance;
        }

        public List<Supplier> GetSuppliers() => MyDB_Context.GetInstance().Suppliers.ToList();
        public Supplier GetCategoryById(string id) => MyDB_Context.GetInstance().Suppliers.SingleOrDefault(c => c.SupplierID == id);

        public void SaveSupplier(Supplier supplier)
        {
            try
            {
                MyDB_Context.GetInstance().Suppliers.Add(supplier);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<Supplier>(supplier).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Suppliers.Remove(supplier);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
