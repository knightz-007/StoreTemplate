using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryDAO
    {

        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }
        public static List<Category> GetCategories()
        {
            var listCategory = new List<Category>();
            try
            {
                using (var context = new MyStoreContext())
                {
                    listCategory = context.Categories.ToList();
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCategory;
        }
    }
}
