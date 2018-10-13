using DataModels.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace DataModels.Dao
{
   public class UserDao
    {
        OnlineShopingDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopingDbContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        // this method to get all users
        public IEnumerable<User> listAllPaging(int page ,int pageSize )
        {
            return db.Users.OrderByDescending(x=>x.CreateDate). ToPagedList(page,pageSize);
        }

        public User GetById (string userName )
        {
            return db.Users.SingleOrDefault(x=>x.UserName==userName);
        }
        public int Login(string userName,string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if(result ==null )
            {
                return 0 ;
            }
            else
            {
                if (result.Status==false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }

        }
    }
}
