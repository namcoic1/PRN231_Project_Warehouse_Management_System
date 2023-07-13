using BusinessObjects;
using DataAccess;

namespace Repositories.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        public void SaveProduct(Product product) => ProductDAO.GetInstance.SaveProduct(product);

        public void DeleteProduct(Product product) => ProductDAO.GetInstance.DeleteProduct(product);

        public Product GetProductById(int id) => ProductDAO.GetInstance.GetProductById(id);

        public Product GetProductByLastId() => ProductDAO.GetInstance.GetProductByLastId();

        public List<Product> GetProducts() => ProductDAO.GetInstance.GetProducts();

        public void UpdateProduct(Product product) => ProductDAO.GetInstance.UpdateProduct(product);
    }
}
