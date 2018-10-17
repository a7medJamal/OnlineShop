using DataModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dao
{
   public  class OrderDao
    {
        OnlineShopingDbContext db = null;
        public OrderDao()
        {
            db = new OnlineShopingDbContext();
        }
       public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;

        }
    }
}
