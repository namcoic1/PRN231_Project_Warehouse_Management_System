﻿using BusinessObjects;

namespace Repositories.ProductRepo
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        Product GetProductByLastId();
        void SaveProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
