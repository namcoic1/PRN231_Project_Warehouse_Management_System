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

        public List<Product> GetProducts() => MyDbContext.GetInstance().Products.ToList();
        public Product GetProductById(int id) => MyDbContext.GetInstance().Products.SingleOrDefault(c => c.ID == id);

        public void SaveProduct(Product product)
        {
            try
            {
                MyDbContext.GetInstance().Products.Add(product);
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Entry<Product>(product).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
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
                MyDbContext.GetInstance().Products.Remove(product);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
