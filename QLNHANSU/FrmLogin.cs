using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNHANSU
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        USER _users = new USER();
        ROLE _roles = new ROLE();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            tb_USER user = new tb_USER();
            user.USERACC = txtUser.Text;
            user.PASSWORD = txtPass.Text;
            string mess = _users.login(user); 
            switch (mess)
            {
                case "login_err":
                    MessageBox.Show("login err!");
                    break;
                default:
                    this.Hide();
                    string TRUYCAP = _roles.getItem_name_truycap(Int32.Parse(mess));
                    MainForm mainForm = new MainForm(TRUYCAP);
                    mainForm.ShowDialog();
                    mainForm = null;
                    this.Close();
                    break;
            }
        }
    }
}