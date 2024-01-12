using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ROLE
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public List<tb_ROLE> getListR()
        {
            return db.tb_ROLE.ToList();
        }
        public string Delete(int id)
        {

            try
            {
                if (id > 1)
                {
                    var _r = db.tb_ROLE.FirstOrDefault(x => x.IDROLE == id);
                    db.tb_ROLE.Remove(_r);
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
        public tb_ROLE Add(tb_ROLE r)
        {
            try
            {
                db.tb_ROLE.Add(r);
                db.SaveChanges();
                return r;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public string getItem_name_truycap(Int32 id)
        {
            return db.tb_ROLE.FirstOrDefault(x => x.IDROLE == id).TRUYCAP;
        }
        public tb_ROLE getItem(int id)
        {
            return db.tb_ROLE.FirstOrDefault(x => x.IDROLE == id);
        }
        public tb_ROLE Update(tb_ROLE r)
        {
            try
            {
                var _r = db.tb_ROLE.FirstOrDefault(x => x.IDROLE == r.IDROLE);
                _r.TENROLE = r.TENROLE;
                _r.TRUYCAP = r.TRUYCAP;
                db.SaveChanges();
                return r;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
    }
}
