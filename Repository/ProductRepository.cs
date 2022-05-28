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
        public DataTransferObject.Product GetProductById(int id) => ProductDAO.Instance.GetProductByID(id);
        public void DeleteProduct(DataTransferObject.Product p) => ProductDAO.Instance.RemoveProduct(p);

        public List<BusinessObject.Category> GetCategories() => CategoryDAO.GetCategories();

        public List<DataTransferObject.Product> GetProducts() => ProductDAO.Instance.GetProductList();

        public void SaveProduct(DataTransferObject.Product p) => ProductDAO.Instance.AddProduct(p);
        public void UpdateProduct(DataTransferObject.Product p) => ProductDAO.Instance.UpdateProduct(p);
    }
}
