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

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.UserName = entity.UserName;
                if (!string .IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Phone = entity.Phone;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedDate = DateTime.Now;
                user.Status = entity.Status;
                db.SaveChanges();
                return true;

            }
            catch (Exception )
            {

                return false;
            }

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

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
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
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
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
