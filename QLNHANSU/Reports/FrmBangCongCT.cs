using BusinessLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNHANSU.Reports
{
    public partial class FrmBangCongCT : DevExpress.XtraEditors.XtraForm
    {
        public FrmBangCongCT()
        {
            InitializeComponent();
        }
        NHANVIEN _nhanvine;
        BANGCONG_NV_CT _bcct_nv;
        

        private void FrmBangCongCT_Load(object sender, EventArgs e)
        {
            _nhanvine = new NHANVIEN();
            _bcct_nv = new BANGCONG_NV_CT();
            loadNhanVien();
            cboKyCong.SelectedIndex = DateTime.Now.Month-1;
        }

        void loadNhanVien()
        {
            cboNhanVien.DataSource = _nhanvine.getList();
            cboNhanVien.DisplayMember = "HOTEN";
            cboNhanVien.ValueMember = "MANV";

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            var lst = _bcct_nv.getBangCongCT(DateTime.Now.Year * 100 + int.Parse(cboKyCong.Text), int.Parse(cboNhanVien.SelectedValue.ToString()));
            rptBangCongCT frp = new rptBangCongCT(lst);
            frp.ShowPreviewDialog();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}