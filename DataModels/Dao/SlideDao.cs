using DataModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dao
{
    public class SlideDao
    {
        OnlineShopingDbContext db = null;
        public SlideDao()
        {
            db = new OnlineShopingDbContext();
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Staus == true).OrderBy(x => x.DisplayOdrder).ToList();
        }
    }
}
