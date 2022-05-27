using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }


        public List<Product> GetProductList()
        {
            var listProducts = new List<Product>();
            try
            {
                using(var context = new MyStoreContext())
                {
                    listProducts = context.Products.ToList();
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        public Product GetProductByID(int productId)
        {
            Product product = new Product();
            try
            {
                using(var context = new MyStoreContext())
                {
                    product = context.Products.SingleOrDefault(x => x.ProductId == productId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public void AddProduct(Product product)
        {
            try
            {
                using(var context = new MyStoreContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
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
                using (var context = new MyStoreContext())
                {
                    context.Entry<Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveProduct(Product product)
        {
            try
            {
                using (var context = new MyStoreContext())
                {
                    var p1 = context.Products.SingleOrDefault(c => c.ProductId == product.ProductId);
                    context.Products.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
