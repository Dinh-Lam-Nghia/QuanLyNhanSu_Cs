using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
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
    public partial class FrmUser : DevExpress.XtraEditors.XtraForm
    {
        public FrmUser()
        {
            InitializeComponent();
        }
        ROLE _role;
        USER _user;
        NHANVIEN _nhanvien;
        bool _them;
        int _id=0;

        private void FrmUser_Load(object sender, EventArgs e)
        {
            _them = false;
            _user = new USER();
            _nhanvien = new NHANVIEN();
            _role = new ROLE();
            _showHide(true);
            loadData();
            loadNhanVien();
            loadRole();
        }
        void loadNhanVien()
        {
            lkNhanVien.Properties.DataSource = _nhanvien.getListFull();
            lkNhanVien.Properties.DisplayMember = "HOTEN";
            lkNhanVien.Properties.ValueMember = "MANV";
        }
        void loadRole()
        {
            cboRole.DataSource = _role.getListR();
            cboRole.DisplayMember = "TENROLE";
            cboRole.ValueMember = "IDROLE";
        }
        void loadData()
        {
            gcUser.DataSource = _user.getList();
            gvUser.OptionsBehavior.Editable = false;
        }
        void _showHide(bool kt)
        {
            gr_addAcc.Enabled = !kt; 
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;
            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnDong.Enabled = kt;
            btnPrint.Enabled = kt;

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _them = true;
            txtUser.Text = string.Empty;
            txtPass.Text = string.Empty;
            lkNhanVien.EditValue = 0;
            cboRole.SelectedIndex = 0;
        }

        private void gcUser_Click(object sender, EventArgs e)
        {
            if (gvUser.RowCount > 0)
            {
                
                _id = int.Parse(gvUser.GetFocusedRowCellValue("IDACC").ToString());
                txtUser.Text = gvUser.GetFocusedRowCellValue("USERACC").ToString();
                txtPass.Text = gvUser.GetFocusedRowCellValue("PASSWORD").ToString();

            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _them = false;
            _showHide(false);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string mess = _user.Delete(_id);
                switch (mess)
                {
                    case "DELETE_ERR":
                        MessageBox.Show("Xoa khong thanh cong!!");
                        break;
                    case "DELETR_OK":
                        break;
                }
                loadData();
            }
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            loadData();
            _them = false;
            _showHide(true);
        }

        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _them = false;
            _showHide(true);
        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        void SaveData()
        {
            if (lkNhanVien.EditValue == null || cboRole.ValueMember == null||txtUser.Text ==""||txtPass.Text=="") return;
            if (_them)
            {
                tb_USER us = new tb_USER();
                us.MANV = int.Parse(lkNhanVien.EditValue.ToString());
                us.USERACC = txtUser.Text;
                us.PASSWORD = txtPass.Text;
                us.TENROLE = int.Parse(cboRole.SelectedValue.ToString()); 
                _user.Add(us);
            }
            else
            {
                var us = _user.getItem(_id);
                us.MANV = int.Parse(lkNhanVien.EditValue.ToString());
                us.USERACC = txtUser.Text;
                us.PASSWORD = txtPass.Text;
                us.TENROLE = int.Parse(cboRole.Text);
                _user.Update(us);
            }
        }
    }
}