using DataModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Dao
{
    public class OrderDetailsDao
    {
        OnlineShopingDbContext db = null;
        public OrderDetailsDao()
        {
            db = new OnlineShopingDbContext();
        }
        public bool Insert (OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
          
        }
    }
}
