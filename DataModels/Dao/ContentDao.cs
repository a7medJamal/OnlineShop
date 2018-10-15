using DataModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dao
{
    public class ContentDao
    {
        OnlineShopingDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopingDbContext();
        }
        public Content GetByID (long id)
        {
            return db.Contents.Find(id);
        }
    }
}
