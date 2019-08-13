using System;

namespace SmartChoiceApp.Models
{
    public class Review
    {
        public string MaBinhLuan { get; set; }
        public string MaNguoiDung { get; set; }
        public string MaLoaiSanPham { get; set; }
        public string TenNguoiDung { get; set; }
        public double Rating { get; set; }
        public string NoiDung { get; set; }
        public string AnhDaiDien { get; set; }
        public string NgayBinhLuan { get; set; }
    }
}
