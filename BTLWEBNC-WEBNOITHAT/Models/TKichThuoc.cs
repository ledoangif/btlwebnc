using System;
using System.Collections.Generic;

namespace BTLWEBNC_WEBNOITHAT.Models
{
    public partial class TKichThuoc
    {
        public TKichThuoc()
        {
            TChiTietSanPhams = new HashSet<TChiTietSanPham>();
        }

        public string MaKichThuoc { get; set; } = null!;
        public string? KichThuoc { get; set; }

        public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; }
    }
}
