using DataModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dao
{
    public class FooterDao
    {
        OnlineShopingDbContext db = null;

        public FooterDao()
        {
            db = new OnlineShopingDbContext();

        }

        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }
    }
}
