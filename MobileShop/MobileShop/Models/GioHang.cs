using System;
using System.Collections.Generic;

namespace MobileShop.Models;

public partial class GioHang
{
    public int MaGioHang { get; set; }

    public int MaKhachHang { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual KhachHang MaKhachHangNavigation { get; set; } = null!;
}
