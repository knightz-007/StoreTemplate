using AutoMapper;
using BusinessObject;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BusinessObject.Product, DataTransferObject.Product>();
            CreateMap<DataTransferObject.Product, BusinessObject.Product>();
            CreateMap<DataTransferObject.Category, BusinessObject.Category>();
            CreateMap<BusinessObject.Category, DataTransferObject.Category>();
        }
    }
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


        public List<DataTransferObject.Product> GetProductList()
        {
            var listProducts = new List<DataTransferObject.Product>();
            var listProducts1 = new List<BusinessObject.Product>();
            try
            {
                using(var context = new MyStoreContext())
                {
                    listProducts1 = context.Products.ToList();
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });
                var mapper = config.CreateMapper();

                // Chuyển đổi danh sách Product qua danh sách ProductDto.
                listProducts = listProducts1.Select
                                 (
                                   emp => mapper.Map<BusinessObject.Product, DataTransferObject.Product>(emp)
                                 ).ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return listProducts;
        }

        public DataTransferObject.Product GetProductByID(int productId)
        {
            DataTransferObject.Product product = new DataTransferObject.Product();
            BusinessObject.Product product1 = new BusinessObject.Product();
            try
            {
                using(var context = new BusinessObject.MyStoreContext())
                {
                    product1 = context.Products.SingleOrDefault(x => x.ProductId == productId);
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile());
                });
                var mapper = config.CreateMapper();

                // Chuyển đổi product qua  productdto.
                product.ProductName = product1.ProductName;
                product.ProductId = product1.ProductId;
                product.UnitPrice = product1.UnitPrice;
                product.CategoryId = product1.CategoryId;
                product.UnitsInStock = product1.UnitsInStock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public void AddProduct(DataTransferObject.Product product)
        {
            try
            {
                BusinessObject.Product product1 = new BusinessObject.Product();
                product1.ProductName = product.ProductName;
                product1.ProductId = product.ProductId;
                product1.UnitPrice = product.UnitPrice;
                product1.CategoryId = product.CategoryId;
                product1.UnitsInStock = product.UnitsInStock;
                using (var context = new MyStoreContext())
                {
                    context.Products.Add(product1);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(DataTransferObject.Product product)
        {
            try
            {
                using (var context = new MyStoreContext())
                {
                    context.Entry<DataTransferObject.Product>(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveProduct(DataTransferObject.Product product)
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
