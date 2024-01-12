using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class USER
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_USER getItem(int id)
        {
            return db.tb_USER.FirstOrDefault(x => x.IDACC == id);
        }
        
        public string login(tb_USER _user)
        {
            try
            {
                var user = db.tb_USER.FirstOrDefault(x => x.USERACC == _user.USERACC);
                if(user != null)
                {
                    if (user.PASSWORD == _user.PASSWORD) {
                        return user.TENROLE.ToString();
                    };
                }
                return "login_err";

            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return "login_err";
            }
        }
        public tb_USER Add(tb_USER us)
        {
            try
            {
                db.tb_USER.Add(us);
                db.SaveChanges();
                return us;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_USER Update(tb_USER us)
        {
            try
            {
                var _us = db.tb_USER.FirstOrDefault(x => x.IDACC == us.IDACC);
                _us.USERACC = us.USERACC;
                db.SaveChanges();
                return us;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public string Delete(int id)
        {
            
            try
            {
                if (id > 1 )
                {
                    var _us = db.tb_USER.FirstOrDefault(x => x.IDACC == id);
                    db.tb_USER.Remove(_us);
                    db.SaveChanges();
                    return "DELETE_OK";
                }
                return "DELETE_ERR";
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
                return "DELETE_ERR";
            }
        }

        public List<tb_USER> getList()
        {
            return db.tb_USER.ToList();
        }
    }
}
