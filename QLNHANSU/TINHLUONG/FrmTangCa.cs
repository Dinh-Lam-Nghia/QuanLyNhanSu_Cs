﻿using BusinessLayer;
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

namespace QLNHANSU.TINHLUONG
{
    public partial class FrmTangCa : DevExpress.XtraEditors.XtraForm
    {
        public FrmTangCa()
        {
            InitializeComponent();
        }
        TANGCA _tangca;
        NHANVIEN _nhanvien;
        LOAICA _loaica;
        SYS_CONFIG _config;
        bool _them;
        int _id;

        private void FrmTangCa_Load(object sender, EventArgs e)
        {
            _them = false;
            _tangca = new TANGCA();
            _loaica = new LOAICA();
            _nhanvien = new NHANVIEN();
            _config = new SYS_CONFIG();
            _showHide(true);
            loadData();
            loadNhanVien();
            loadLoaiCa();
        }
        void _showHide(bool kt)
        {
            btnLuu.Enabled = !kt;
            btnHuy.Enabled = !kt;
            btnThem.Enabled = kt;
            btnSua.Enabled = kt;
            btnXoa.Enabled = kt;
            btnDong.Enabled = kt;
            btnPrint.Enabled = kt;
            txtNoiDung.Enabled = !kt;
            spSoGio.Enabled = !kt;
            lkNhanVien.Enabled = !kt;
            cboLoaiCa.Enabled = !kt;
        }
        void loadNhanVien()
        {
            lkNhanVien.Properties.DataSource = _nhanvien.getListFull();
            lkNhanVien.Properties.DisplayMember = "HOTEN";
            lkNhanVien.Properties.ValueMember = "MANV";
        }
        void loadLoaiCa()
        {
            cboLoaiCa.DataSource = _loaica.getList();
            cboLoaiCa.DisplayMember = "TENLOAICA";
            cboLoaiCa.ValueMember = "IDLOAICA";
        }


        void loadData()
        {
            gcDanhSach.DataSource = _tangca.getListFull();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _them = true;
            txtNoiDung.Text = string.Empty;
            spSoGio.EditValue = 0;
            lkNhanVien.EditValue = 0;
            cboLoaiCa.SelectedIndex = 0;
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _them = false;
            _showHide(false);
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_id < 1) return;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _tangca.Delete(_id, 1);
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

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        void SaveData()
        {
            if (_them)
            {
                tb_TANGCA tc = new tb_TANGCA();
                tc.IDLOAICA = int.Parse(cboLoaiCa.SelectedValue.ToString());
                tc.SOGIO = double.Parse(spSoGio.EditValue.ToString());
                tc.MANV = int.Parse(lkNhanVien.EditValue.ToString());
                tc.GHICHU = txtNoiDung.Text;
                tc.NGAY = DateTime.Now.Day;
                tc.THANG = DateTime.Now.Month;
                tc.NAM = DateTime.Now.Year;
                var lc = _loaica.getItem(int.Parse(cboLoaiCa.SelectedValue.ToString()));
                var cg = _config.getItem("TANGCA");
                tc.SOTIEN = tc.SOGIO * lc.HESO * int.Parse(cg.Value);
                tc.CREATED_BY = 1;
                tc.CREATED_DATE = DateTime.Now;
                _tangca.Add(tc);
            }
            else
            {
                var tc = _tangca.getItem(_id);
                tc.IDLOAICA = int.Parse(cboLoaiCa.SelectedValue.ToString());
                tc.SOGIO = double.Parse(spSoGio.EditValue.ToString());
                tc.MANV = int.Parse(lkNhanVien.EditValue.ToString());
                tc.GHICHU = txtNoiDung.Text;
                tc.NGAY = DateTime.Now.Day;
                tc.THANG = DateTime.Now.Month;
                tc.NAM = DateTime.Now.Year;
                var lc = _loaica.getItem(int.Parse(cboLoaiCa.SelectedValue.ToString()));
                var cg = _config.getItem("TANGCA");
                tc.SOTIEN = tc.SOGIO * lc.HESO * int.Parse(cg.Value);
                tc.UPDATED_BY = 1;
                tc.UPDATED_DATE = DateTime.Now;
                _tangca.Update(tc);
            }
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _id = int.Parse(gvDanhSach.GetFocusedRowCellValue("ID").ToString());
                txtNoiDung.Text = gvDanhSach.GetFocusedRowCellValue("GHICHU").ToString();
                spSoGio.Text = gvDanhSach.GetFocusedRowCellValue("SOGIO").ToString();
                lkNhanVien.EditValue = gvDanhSach.GetFocusedRowCellValue("MANV").ToString();
                cboLoaiCa.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDLOAICA").ToString();
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DELETED_BY" && e.CellValue != null)
            {
                Image img = Properties.Resources.delete_x16;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }
    }

}