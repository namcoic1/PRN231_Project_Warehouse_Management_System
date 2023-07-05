using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO _instance = null;
        private ProductDAO() { }
        public static ProductDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProductDAO();
            }
            return _instance;
        }

        public List<Product> GetProducts() => MyDB_Context.GetInstance().Products.ToList();
        public Product GetProductById(int id) => MyDB_Context.GetInstance().Products.SingleOrDefault(c => c.ProductID == id);

        public void SaveProduct(Product product)
        {
            try
            {
                MyDB_Context.GetInstance().Products.Add(product);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateProduct(Product product)
        {
            try
            {
                MyDB_Context.GetInstance().Entry<Product>(product).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteProduct(Product product)
        {
            try
            {
                MyDB_Context.GetInstance().Products.Remove(product);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
