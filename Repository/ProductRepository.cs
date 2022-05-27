using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product p) => ProductDAO.Instance.RemoveProduct(p);

        public List<Category> GetCategories() => CategoryDAO.GetCategories();

        public Product GetProductById(int id) => ProductDAO.Instance.GetProductByID(id);

        public List<Product> GetProducts() => ProductDAO.Instance.GetProductList();

        public void SaveProduct(Product p) => ProductDAO.Instance.AddProduct(p);
        public void UpdateProduct(Product p) => ProductDAO.Instance.UpdateProduct(p);
    }
}
