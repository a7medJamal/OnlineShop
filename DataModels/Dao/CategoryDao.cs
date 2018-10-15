using DataModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dao
{
   public  class CategoryDao
    {
        OnlineShopingDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopingDbContext();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
    }
}
