using DataModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dao
{
    public class MenuDao
    {
        OnlineShopingDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopingDbContext();
        }

        public List<Menu> ListByGroupID(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId &&x.Staus==true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
