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
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace QLNHANSU
{
    public partial class FrmPhanQuyen : DevExpress.XtraEditors.XtraForm
    {
        public FrmPhanQuyen()
        {
            InitializeComponent();
        }
        ROLE _role;
        USER _user;
        NHANVIEN _nhanvien;
        bool _them;
        int _id;
        private void FrmPhanQuyen_Load(object sender, EventArgs e)
        {
            _them = false;
            _user = new USER();
            _nhanvien = new NHANVIEN();
            _role = new ROLE();
            _showHide(true);
            loadData();
        }
        void loadData()
        {
            gcRole.DataSource = _role.getListR();
            gvRole.OptionsBehavior.Editable = false;
        }
        void _showHide(bool kt)
        {
            gr_addRole.Enabled = !kt;
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;
            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnDong.Enabled = kt;
            btnPrint.Enabled = kt;
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_id == null) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string mess = _role.Delete(_id);
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

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _them = true;
            txtRole.Text = string.Empty;
            check_ht.Checked = false;
            check_dm.Checked = false;
            check_cc.Checked = false;
            check_bb.Checked = false;
        }

        private void btnLuu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
            loadData();
            _them = false;
            _showHide(true);
        }
        void SaveData()
        {
            string ht = null;
            string dm = null;
            string cc = null;
            string bb = null;
            if (check_ht.Checked)
            {
                ht = "he_thong,";
            }
            if (check_dm.Checked)
            {
                dm = "danh_muc,";
            }
            if (check_cc.Checked)
            {
                cc = "cham_cong,";
            }
            if (check_bb.Checked)
            {
                bb = "bao_bieu,";
            }
            if (_them)
            {
                if (txtRole.Text == "") return;
                tb_ROLE r = new tb_ROLE();
                r.TENROLE = txtRole.Text;
                r.TRUYCAP = ht + dm + cc + bb;
                _role.Add(r);
            }
            else
            {
                if (txtRole.Text == "") return;
                var r = _role.getItem(_id);
                r.TENROLE = txtRole.Text;
                r.TRUYCAP = ht + dm + cc + bb;
                _role.Update(r);
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _them = false;
            _showHide(false);
        }

        private void gcRole_Click(object sender, EventArgs e)
        {
            if (gvRole.RowCount > 0)
            {
                _id = int.Parse(gvRole.GetFocusedRowCellValue("IDROLE").ToString());
                txtRole.Text = gvRole.GetFocusedRowCellValue("TENROLE").ToString();
                string TRUYCAP = gvRole.GetFocusedRowCellValue("TRUYCAP").ToString();
                if(TRUYCAP.IndexOf("he_thong") != -1)
                {
                    check_ht.Checked = true;
                }
                if (TRUYCAP.IndexOf("danh_muc") != -1)
                {
                    check_dm.Checked = true;
                }
                if (TRUYCAP.IndexOf("cham_cong") != -1)
                {
                    check_cc.Checked = true;
                }
                if (TRUYCAP.IndexOf("bao_bieu") != -1)
                {
                    check_bb.Checked = true;
                }
            }
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

        private void check_ht_CheckedChanged(object sender, EventArgs e)
        {
check_ht.BackColor = Color.Red;
        }
    }
}