﻿using BusinessLayer;
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

namespace QLNHANSU.TINHLUONG
{
    public partial class FrmBangLuong : DevExpress.XtraEditors.XtraForm
    {
        public FrmBangLuong()
        {
            InitializeComponent();
        }
        BANGLUONG _bangluong;
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void FrmBangLuong_Load(object sender, EventArgs e)
        {
            _bangluong = new BANGLUONG();
            cboNam.Text = DateTime.Now.Year.ToString();
            cboThang.Text = DateTime.Now.Month.ToString();
            
        }
        void loadData()
        {
            gcDanhSach.DataSource = _bangluong.getList(int.Parse(cboNam.Text)* 100 + int.Parse(cboThang.Text));
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        private void btnTinhLuong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _bangluong.TinhLuongNhanVien(int.Parse(cboNam.Text) * 100 + int.Parse(cboThang.Text));
            loadData();
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnXemBangLuong_Click(object sender, EventArgs e)
        {
            loadData();
        }
    }
}