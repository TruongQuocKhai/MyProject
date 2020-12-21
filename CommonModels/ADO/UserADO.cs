using CommonModels.EF;
using CommonL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace CommonModels.ADO
{
    public class UserADO
    {
        MyProjectDb db = null;
        public UserADO()
        {
            db = new MyProjectDb();
        }

        public int SignIn(string email, string password, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.Email == email);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstDecentralization.ADMIN_GROUP || result.GroupID == CommonConstDecentralization.MOD_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;  // Account locked
                        }
                        else
                        {
                            if (result.Password == password)
                                return 1; // Login correctly
                            else
                                return -2; // Incorrect password
                        }
                    }
                    else
                    {
                        return -3; // Login incorrect
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == password)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }

        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }
        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public IEnumerable<User> GetIdUser(string id)
        {
            return db.Users.Where(x => x.ID.ToString() == id).ToList();
        }

        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<User> GetListUsers(ref int totalRecord, int page, int pageSize)
        {
            totalRecord = db.Users.Count();
            return db.Users.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Phone = entity.Phone;
                user.Address = entity.Address;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool DeleteUser(long id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<string> GetListCredential(string userEmail)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == userEmail);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.Email
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }

        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
    }
}