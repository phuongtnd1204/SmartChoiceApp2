using System;
using System.Collections.Generic;
using System.Text;

namespace SmartChoiceApp.Models
{
    public class ProductInfo
    {
        public int MaSanPham { get; set; }
        public int MaNSX { get; set; }
        public int MaLoaiSanPham { get; set; }
        public string TenGiong { get; set; }
        public string TenNSX { get; set; }
        public string DiaChiNSX { get; set; }
        public int SDT { get; set; }
        public string HinhAnhNSX { get; set; }
        public string GiayChungNhan { get; set; }
        public string KyThuatTrong { get; set; }
        public int ThoiGianTangTruong { get; set; }
        public int SoLanQuet { get; set; }
        //public int NgayTrong { get; set; }
        public string NgayTrong { get; set; }
        public string DiaChiTrong { get; set; }
        public string NgayThuHoach { get; set; }
        public int HanSuDung { get; set; }
        public string PhanBon { get; set; }
        public string HinhAnhSanPham { get; set; }
        public int SoCayTrong { get; set; }
        public string TenNhaCungCap { get; set; }
        public double DanhGia { get; set; }
    }
}
