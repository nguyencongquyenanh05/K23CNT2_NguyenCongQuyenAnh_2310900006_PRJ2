using System;
using System.Collections.Generic;

namespace MobileShop.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public string? TenSp { get; set; }

    public decimal? Gia { get; set; }

    public string? MoTa { get; set; }

    public int? MaDanhMuc { get; set; }

    public string? HinhAnh { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietSanPham> ChiTietSanPhams { get; set; } = new List<ChiTietSanPham>();

    public virtual DanhMuc? MaDanhMucNavigation { get; set; }
}
