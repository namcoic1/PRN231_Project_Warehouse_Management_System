using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO _instance = null;
        private static MyDbContext _context = null;
        private ProductDAO()
        {
        }
        public static ProductDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProductDAO();
                }

                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<Product> GetProducts() => _context.Products.Include(c => c.Category).Include(s => s.Supplier).ToList();
        public Product GetProductById(int id) => _context.Products.Include(c => c.Category).Include(s => s.Supplier).SingleOrDefault(c => c.Id == id);
        public Product GetProductByLastId() => _context.Products.Include(c => c.Category).Include(s => s.Supplier).OrderBy(c => c.Id).LastOrDefault();

        public void SaveProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
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
                _context.Entry<Product>(product).State = EntityState.Modified;
                _context.SaveChanges();
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
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
