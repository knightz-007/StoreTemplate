using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        void SaveProduct(DataTransferObject.Product p);
        DataTransferObject.Product GetProductById(int id);
        void DeleteProduct(DataTransferObject.Product p);
        void UpdateProduct(DataTransferObject.Product p);
        List<BusinessObject.Category> GetCategories();
        List<DataTransferObject.Product> GetProducts();
    }
}
